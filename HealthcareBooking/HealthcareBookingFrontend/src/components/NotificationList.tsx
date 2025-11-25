import { type Notify } from "../types/Notify";

interface Props {
  notifications: Notify[];
  activeId?: string;
  onSelect: (n: Notify) => void;
}

const NotificationList: React.FC<Props> = ({ notifications, activeId, onSelect }) => {
  
  return (
    <div className="flex flex-col gap-3">
      {notifications.map((n) => (
        <div
          key={n.notifyStaffId}
          onClick={() => onSelect(n)}
          className={`p-3 rounded-lg cursor-pointer border 
          ${activeId === n.notifyStaffId 
            ? "border-rose-600 bg-rose-50" 
            : "border-gray-200 hover:bg-gray-100"}`}
        >
          <p className="text-sm text-gray-900 font-medium">{n.message}</p>
          <p className="text-xs text-gray-500">
            {new Date(n.createdAt).toLocaleString()}
          </p>
          {/* <p>{n.relatedBooking.bookingStatus}</p> */}
        </div>
      ))}
    </div>
  );
};

export default NotificationList;
