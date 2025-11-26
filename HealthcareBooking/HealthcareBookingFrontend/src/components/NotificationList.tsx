import { type Notify } from "../types/Notify";

interface Props {
  notifications: Notify[];
  activeId?: string;
  onSelect: (n: Notify) => void;
}

const NotificationList: React.FC<Props> = ({ notifications, activeId, onSelect }) => {
  const divClasses = `p-3 rounded-lg cursor-pointer border`;
  return (
    <div className="flex flex-col gap-3">
      {notifications.map((n) => (
        <div
          key={n.notifyStaffId}
          onClick={() => onSelect(n)}
          className={`
          ${activeId === n.notifyStaffId 
            ? "border-rose-600 bg-rose-50" 
            : "border-gray-200 hover:bg-gray-100"}
            ${n.notificationStatus === "Sent" ? "bg-amber-100/80":""} ` + divClasses}
        >
          <p className="text-sm text-gray-900 font-medium">{n.message}</p>
          <p className="text-xs text-gray-500">
            {new Date(n.createdAt).toLocaleString()}
          </p>
        </div>
      ))}
    </div>
  );
};

export default NotificationList;

// export const GetExternal = async (url: string, tutorial:string) => {
//   try {
//     const res = await fetch(url + tutorial);
//     const data = await res.json();
//     return data;
//   } catch (error) {
//     console.error("Error fetching external data:", error);
//     throw error;
//   }
//  }
// class ExternalApi {
//   fecthData = async (url: string) => {
//     try {
//       const res = await GetExternal(url, "react-tutorial");
//       return {res}
//     } catch (error) {
//       console.error("Error in fetchData:", error);
//       throw error;
//     }
//   }

//   render() {
//     const htmlString: any = this.fecthData("https://api.sampleapis.com/tutorials/").then((result) => {

//     })
//     return (
//       <div dangerouslySetInnerHTML={{ __html: htmlString }} />
//     );
//   }
// }