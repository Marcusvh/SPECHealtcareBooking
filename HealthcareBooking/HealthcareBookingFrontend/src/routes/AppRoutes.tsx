import { Routes, Route } from "react-router-dom";
import Home from "../pages/Home";
import Booking from "../pages/BookingAppointment";
import { StaffBookingConfirmation } from "../pages/StaffBookingConfirmation";
import { Contact } from "../pages/Contact";
// import MyBookings from "../pages/MyBookings";
// import BookingDetails from "../pages/BookingDetails";

export default function AppRoutes() {
    return (
        <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/booking" element={<Booking />} />
            <Route path="/contact" element={<Contact />} />
            {/*<Route path="/my-bookings" element={<MyBookings />} />
            <Route path="/booking/:id" element={<BookingDetails />} /> */}
            <Route path="/staff-booking-confirmation" element={<StaffBookingConfirmation />} />
        </Routes>
    );
}
