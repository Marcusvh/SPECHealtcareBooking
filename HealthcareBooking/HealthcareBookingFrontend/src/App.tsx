import './App.css'
import AppRoutes from "./routes/AppRoutes";
import Header from './components/Header';
function App() {
    return (
        <div style={{ fontFamily: "Inter, sans-serif" }} className="min-h-screen bg-gray-100">
            <Header></Header>

            <AppRoutes />
        </div>
    );
}

export default App
