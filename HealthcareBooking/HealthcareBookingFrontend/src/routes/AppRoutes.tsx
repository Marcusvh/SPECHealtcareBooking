import { Routes, Route } from "react-router-dom";
import Home from "../pages/Home";
// import BookingForm from "../pages/BookingForm";
// import MyBookings from "../pages/MyBookings";
// import BookingDetails from "../pages/BookingDetails";

export default function AppRoutes() {
    return (
        <Routes>
            <Route path="/" element={<Home />} />
            {/* <Route path="/booking" element={<BookingForm />} />
            <Route path="/my-bookings" element={<MyBookings />} />
            <Route path="/booking/:id" element={<BookingDetails />} /> */}
        </Routes>
    );
}
