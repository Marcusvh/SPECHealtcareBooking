import { Link } from "react-router-dom";

const NavBar: React.FC = () => {
  return (
    <nav className="flex items-center gap-10 text-lg">
        <Link to={"/"} className="hover:text-rose-600 transition">Home</Link>
        <Link to={"/Booking"} className="hover:text-rose-600 transition">Book appointment</Link>
        <Link to={"/"} className="hover:text-rose-600 transition">Clinics</Link>
        <Link to={"/"} className="hover:text-rose-600 transition">Contact</Link>
    </nav>
  );
};

export default NavBar;
