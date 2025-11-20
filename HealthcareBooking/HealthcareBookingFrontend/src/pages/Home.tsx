import React from "react";
import Header from "../components/Header";
import dummyDokter1 from "../assets/images/dummyDokter1.jpg";
import dummyDokter2 from "../assets/images/dummyDokter2.png";
import dummyPainDokter from "../assets/images/dummyPainDokter.jpg";
import heroImg from "../assets/images/heroBanner1.jpg";
import heroImg2 from "../assets/images/heroBanner2.jpeg";

const Home: React.FC = () => {
    const dokterPics = [dummyDokter1, dummyDokter2, dummyPainDokter];

  return (
    <div className="w-full bg-gray-50 text-gray-900">

      {/* MAIN CONTAINER */}
      <div className="max-w-[1280px] mx-auto px-6">

        {/* HERO SECTION */}
        <section className={`flex items-center justify-between py-24`}>
          <div className="max-w-xl space-y-6">
            <h1 className="text-5xl font-bold leading-tight">
              Book Your <span className="text-rose-600">Healthcare</span> Appointment Online
            </h1>
            <p className="text-lg text-gray-600">
              Find trusted doctors, schedule appointments instantly, and get the care you need—fast.
            </p>

            {/* SEARCH BAR */}
            <div className="flex bg-white shadow-lg rounded-lg p-4 gap-4 items-center">
              <input
                type="text"
                placeholder="Search doctors, specialties, clinics..."
                className="flex-1 border border-gray-200 rounded-lg px-4 py-2 focus:outline-rose-500"
              />
              <button className="bg-rose-600 text-white px-6 py-2 rounded-lg hover:bg-rose-700 transition">
                Search
              </button>
            </div>
          </div>

          <div className="hidden lg:block">
            <img
              src={`${heroImg2}`}
              alt="hero"
              className="w-[480px] h-auto"
            />
          </div>
        </section>

        {/* FEATURED DOCTORS */}
        <section className="py-16">
          <h2 className="text-3xl font-bold mb-8">Featured Doctors</h2>

          <div className="grid grid-cols-3 gap-8">
            {dokterPics.map((i) => (
              <div
                key={i}
                className="bg-white shadow rounded-xl p-6 hover:shadow-lg transition"
              >
                <img
                  src={`${i}`}
                  alt="doctor"
                  className="w-fit mx-auto h-[50vh] h-56 object-cover rounded-lg mb-4"
                />

                <h3 className="text-xl font-semibold">Dr. Jane Doe</h3>
                <p className="text-rose-600 font-medium">Cardiologist</p>
                <p className="text-sm text-gray-600 mt-2">
                  10+ years experience • City Medical Center
                </p>

                <button className="mt-4 w-full bg-rose-600 text-white py-2 rounded-lg hover:bg-rose-700 transition">
                  Book Appointment
                </button>
              </div>
            ))}
          </div>
        </section>

        {/* HOW IT WORKS */}
        <section className="py-20">
          <h2 className="text-3xl font-bold text-center mb-12">How It Works</h2>

          <div className="grid grid-cols-3 gap-12 text-center">
            <div className="space-y-4">
              <div className="text-rose-600 text-5xl">🔍</div>
              <h3 className="text-xl font-semibold">Search</h3>
              <p className="text-gray-600">Find doctors by specialty, location, or availability.</p>
            </div>

            <div className="space-y-4">
              <div className="text-rose-600 text-5xl">📅</div>
              <h3 className="text-xl font-semibold">Choose a Time</h3>
              <p className="text-gray-600">Pick the perfect timeslot for your appointment.</p>
            </div>

            <div className="space-y-4">
              <div className="text-rose-600 text-5xl">👍</div>
              <h3 className="text-xl font-semibold">Book Instantly</h3>
              <p className="text-gray-600">Confirm your appointment in just one tap.</p>
            </div>
          </div>
        </section>

        {/* TESTIMONIALS */}
        <section className="py-20">
          <h2 className="text-3xl font-bold mb-8 text-center">What Patients Say</h2>

          <div className="grid grid-cols-3 gap-8">
            {[1, 2, 3].map((i) => (
              <div
                key={i}
                className="bg-white shadow p-6 rounded-xl hover:shadow-lg transition"
              >
                <p className="text-gray-700 italic">
                  “Amazing service! Booking an appointment was easy and quick.”
                </p>
                <div className="mt-4 font-semibold text-rose-600">
                  — Patient {i}
                </div>
              </div>
            ))}
          </div>
        </section>

      </div>

      {/* FOOTER */}
      <footer className="bg-gray-900 text-white py-10 mt-12">
        <div className="max-w-[1280px] mx-auto px-6 flex justify-between">
          <div>
            <h3 className="text-xl font-bold mb-2">HealthBook</h3>
            <p className="text-gray-400 max-w-sm">
              Simple. Fast. Reliable healthcare appointment booking.
            </p>
          </div>

          <div className="space-y-2">
            <h4 className="font-semibold">Quick Links</h4>
            <p className="text-gray-400 hover:text-white cursor-pointer">Doctors</p>
            <p className="text-gray-400 hover:text-white cursor-pointer">Clinics</p>
            <p className="text-gray-400 hover:text-white cursor-pointer">Support</p>
          </div>

          <div className="space-y-2">
            <h4 className="font-semibold">Follow Us</h4>
            <div className="flex gap-4 text-xl">
              <span className="cursor-pointer hover:text-rose-400">🌐</span>
              <span className="cursor-pointer hover:text-rose-400">🐦</span>
              <span className="cursor-pointer hover:text-rose-400">📘</span>
            </div>
          </div>
        </div>
      </footer>

    </div>
  );
};

export default Home;
