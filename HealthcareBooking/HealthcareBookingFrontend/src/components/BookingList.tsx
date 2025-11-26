import type { BookingView } from "../types/bookingView";
import { useState } from "react";

interface Props {
  bookings: BookingView[];
  activeId?: string;
  onSelect: (n: BookingView) => void;
}

const BookingList: React.FC<Props> = ({ bookings, activeId, onSelect }) => {
  const divClasses = `p-3 rounded-lg cursor-pointer border`;
  const [bookingViewFiltered, setBookingViewFiltered] = useState<BookingView[]>([bookings].flat());
  const filtered = (e:any) => {
        setBookingViewFiltered(bookings.filter(bv => bv.bookingStatus === e.target.value || e.target.value === "All"));
  }
  
  return (
    <div className="flex flex-col gap-3">
        <select 
        onChange={(e) => { filtered(e);}}
        className="w-full border rounded-lg px-4 py-2 mb-4" >
          <option value="All">All Statuses</option>
          <option value="Scheduled">Scheduled</option>
          <option value="Completed">Completed</option>
          <option value="Cancelled">Cancelled</option>
        </select>

      {bookingViewFiltered.map((n) => (
        <div
          key={n.detailedBookingViewid}
          onClick={() => onSelect(n)}
          className={`
          ${activeId === n.detailedBookingViewid 
            ? "border-rose-600 bg-rose-50" 
            : "border-gray-200 hover:bg-gray-100"}
            ` + divClasses}
        >
          <p className="text-sm text-gray-900 font-medium mb-2">
            <span className={`px-2 py-1 rounded text-sm 
                ${n?.bookingStatus === "Scheduled" ? "bg-yellow-100 text-yellow-700" : ""}
                ${n?.bookingStatus === "Completed" ? "bg-green-100 text-green-700" : ""}
                ${n?.bookingStatus === "Cancelled" ? "bg-red-100 text-red-700" : ""}
                `}>
                {n?.bookingStatus}
            </span>
          </p>
          <p className="text-sm text-gray-900 font-medium">Patient: {n.patientFullName}</p>
          <p className="text-xs text-gray-500">
            {new Date(n.startTime).toLocaleString()}
          </p>
        </div>
      ))}
    </div>
  );
};

export default BookingList;
