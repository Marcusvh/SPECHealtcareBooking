import { Link } from "react-router-dom";
import NavBar from "./Nav";

const Header: React.FC = () => {
    return (
        <header className="shadow-md border-b border-gray-500 rounded-md bg-white">
            <div className="max-w-[1280px] mx-auto px-6 h-20 flex items-center justify-between">
            <Link to={"/"} className="text-2xl font-black text-rose-600 hover:text-rose-700">HealthBook</Link>
                <NavBar></NavBar>

            <Link to={"/Signin"} className="bg-rose-600 text-white px-5 py-2 rounded-lg hover:bg-rose-700 transition">
                Sign In
            </Link>
            </div>
        </header>
    )
}

export default Header