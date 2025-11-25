import React, { useEffect, useState } from "react";
import { apiRequest } from "../apiRequest/HealthcareApi.ts";
import { type Doctor } from "../types/Doctor.ts";
import { type BookingType } from "../types/BookingType.ts";
import BookingTimeBtn from "../components/BookingTimeBtn.tsx";
import { type Booking } from "../types/Booking.ts";

const BookingAppointment: React.FC = () => {

  const [allDoctors, setAllDoctors] = useState<Doctor[]>([]);
  const [doctors, setDoctors] = useState<Doctor[]>([]);
  const [bookingTypes, setBookingTypes] = useState<BookingType[]>([]);

    // form input
  const [date, setDate] = useState("");
  const [time, setTime] = useState("");
  const [bookingType, setBookingType] = useState("");
  const [doctorId, setDoctorId] = useState<string | null>(null);
  const [notes, setNotes] = useState("");

  let bookingTimes = ["08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00"];
  useEffect(() => {
    const load = async () => {
      try {
        const doctorsData = await apiRequest("Staff/doctor", "GET");
        const bookingTypeData = await apiRequest("Booking/bookingType", "GET");
        console.log(doctorsData);
        
        setDoctors(doctorsData);
        setAllDoctors(doctorsData);
        setBookingTypes(bookingTypeData);
      } catch (err) {
        console.error(err);
      }
    };
    load();
  }, []);

  useEffect(() => {
    if (!bookingType) {
      setDoctors(allDoctors);
      return;
    }

    const filtered = allDoctors.filter(d =>
      d.supportedBookingTypeIds.includes(bookingType)
    );
    setDoctors(filtered);
  }, [bookingType, allDoctors]);

  useEffect(() => {
    if (doctorId && !doctors.some(d => d.staffId === doctorId)) {
      setDoctorId(null);
    }
  }, [doctors]);


  const submitBooking = async () => {
    const [year, month, day] = date.split("-").map(Number);
    const [hour, minute] = time.split(":").map(Number);

    const body: Booking = {
      startTime: new Date(year, month - 1, day, hour, minute),
      bookingTypeId: bookingType,
      patientId: "028ef28d-86f8-40c3-a042-7efad18422f0", // Replace with actual user ID from auth context
      staffId: doctorId ?? undefined,
      patientNotes: notes || undefined
    };

    console.log("Submitting booking:", body);

    try {
      const result = await apiRequest("Booking/booking", "POST", body);
      console.log(result);
      alert("Booking created!");
    } catch (err) {
      console.error(err);
      alert("Something went wrong.");
    }
  };

  return (
    <div className="w-full bg-gray-50 min-h-screen py-20">
<style>
  {`
    .bookingTimebtnActive {
        background-color: #be123c;
    }
  `}
</style>
      <div className="max-w-xl mx-auto bg-white shadow-lg rounded-xl p-10">

        <h1 className="text-3xl font-bold mb-6 text-center">
          Book an Appointment
        </h1>

        <fieldset className="">
            <div>
                <label className="block mb-4">
                <span className="text-gray-700 font-medium">Date</span>
                <input
                    type="date"
                    value={date}
                    onChange={(e) => setDate(e.target.value)}
                    className="w-full border rounded-lg px-4 py-2 mt-1"
                />
                </label>
            </div>
            <div className="mb-4">
                <label className="mb-4">
                  <span className="text-gray-700 font-medium">Time</span>
                </label>
                  <div className="flex flex-wrap gap-4">
                    {bookingTimes.map((t, index) => (
                      <BookingTimeBtn key={index} title={t} onClick={() => setTime(t)} />
                    ))}
                  </div>
            </div>
            
        </fieldset>
        

        {/* BOOKING TYPE */}
        <label className="block mb-4">
          <span className="text-gray-700 font-medium">Appointment Type</span>
          <select
            value={bookingType}
            onChange={(e) => setBookingType(e.target.value)}
            className="w-full border rounded-lg px-4 py-2 mt-1"
          >
            <option value="">Select type...</option>
            {bookingTypes.map((b) => (
              <option key={b.bookingTypeId} value={b.bookingTypeId}>
                {b.name}
              </option>
            ))}
          </select>
        </label>

        {/* OPTIONAL DOCTOR */}
        <label className="block mb-4">
          <span className="text-gray-700 font-medium">Doctor (optional)</span>
          <select
            value={doctorId ?? ""}
            onChange={(e) => setDoctorId(e.target.value || null)}
            className="w-full border rounded-lg px-4 py-2 mt-1"
          >
            <option value="">No preference</option>
            {doctors.map((d) => (
              <option key={d.staffId} value={d.staffId}>
                {d.name} — {d.specialties}
              </option>
            ))}
          </select>
        </label>

        {/* NOTES */}
        <label className="block mb-6">
          <span className="text-gray-700 font-medium">Notes (optional)</span>
          <textarea
            value={notes}
            onChange={(e) => setNotes(e.target.value)}
            rows={3}
            className="w-full border rounded-lg px-4 py-2 mt-1 resize-none"
            placeholder="Describe symptoms, concerns, or preferences..."
          />
        </label>

        <button
          onClick={submitBooking}
          className="w-full bg-rose-700 text-white py-3 rounded-lg hover:bg-rose-600 transition text-lg"
        >
          Confirm Booking
        </button>
      </div>
    </div>
  );
};

export default BookingAppointment;
