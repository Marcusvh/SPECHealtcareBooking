import React, { useEffect, useState } from "react";
import { apiRequest } from "../apiRequest/HealthcareApi";
import { type Doctor } from "../types/Doctor";
import { type BookingType } from "../types/BookingType.ts";

const Booking: React.FC = () => {

  const [doctors, setDoctors] = useState<Doctor[]>([]);
  const [bookingTypes, setBookingTypes] = useState<BookingType[]>([]);

    // form input
  const [date, setDate] = useState("");
  const [time, setTime] = useState("");
  const [bookingType, setBookingType] = useState("");
  const [doctorId, setDoctorId] = useState<string | null>(null);
  const [notes, setNotes] = useState("");

  useEffect(() => {
    const load = async () => {
      try {
        const doctorsData = await apiRequest("Staff/doctor", "GET");
        const bookingTypeData = await apiRequest("Booking/bookingType", "GET");
        console.log(bookingTypeData);
        
        setDoctors(doctorsData);
        setBookingTypes(bookingTypeData);
      } catch (err) {
        console.error(err);
      }
    };
    load();
  }, []);

  const submitBooking = async () => {
    const body = {
      date,
      time,
      bookingType,
      doctorId,
      notes
    };

    console.log("Submitting booking:", body);

    try {
      const result = await apiRequest("booking", "POST", body);
      console.log(result);
      alert("Booking created!");
    } catch (err) {
      console.error(err);
      alert("Something went wrong.");
    }
  };

  return (
    <div className="w-full bg-gray-50 min-h-screen py-20">
      <div className="max-w-xl mx-auto bg-white shadow-lg rounded-xl p-10">

        <h1 className="text-3xl font-bold mb-6 text-center">
          Book an Appointment
        </h1>

        <fieldset className="flex">
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
            <div className="ml-[2rem]">
                <label className="flex flex-col mb-4">
                <span className="text-gray-700 font-medium">Time</span>
                <input
                    type="time"
                    value={time}
                    onChange={(e) => setTime(e.target.value)}
                    className="w-fit border rounded-lg px-4 py-2 mt-1"
                />
                </label>
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
              <option key={b.bookingTypeId} value={b.name}>
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
          className="w-full bg-rose-600 text-white py-3 rounded-lg hover:bg-rose-700 transition text-lg"
        >
          Confirm Booking
        </button>
      </div>
    </div>
  );
};

export default Booking;
