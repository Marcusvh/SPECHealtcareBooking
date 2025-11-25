import { useState, useEffect, use } from "react";
import { apiRequest } from "../apiRequest/HealthcareApi";
import { type Notify } from "../types/Notify";
import NotificationList from "../components/NotificationList";
import BookingDetails from "../components/BookingDetails";
import type { BookingView } from "../types/bookingView";

export const StaffBookingConfirmation: React.FC = () => {
    const [bookingView, setBookingView] = useState<BookingView[]>([]);
    const [selectedBookingView, setSelectedBookingView] = useState<BookingView>();
    const [notifications, setNotifications] = useState<Notify[]>([]);
    const [activeNotification, setActiveNotification] = useState<Notify | null>(null);

    useEffect(() => {
        // Fetch bookings
        apiRequest("Booking/booking/detailedViews", "GET").then((data) => {
            console.log(data);
            setBookingView(data);
        }).catch((error) => {
            console.error("Error fetching bookings:", error);
        });
        // Fetch notification
        apiRequest("Notify/staff", "GET").then((data) => {
            console.log(data);
            setNotifications(data);
            setActiveNotification(data);
        }).catch((error) => {
            console.error("Error fetching notification:", error);
        });
    }, []);

    useEffect(() => {
    if (activeNotification) {
      const relatedBooking = bookingView.find(
        (bv) => bv.detailedBookingViewid === activeNotification.relatedBookingId
      );
      if (relatedBooking) {
        setSelectedBookingView(relatedBooking);
      }
    }}, [activeNotification, bookingView]);
    
    const setActiveNotificationHandler = (notification: Notify) => {
      if(activeNotification?.notifyStaffId === notification.notifyStaffId) setActiveNotification(null);
      else setActiveNotification(notification);
      // markRead(notification.notifyStaffId);
    }

    const rejectBooking = async (bookingId: string, notifyStaffId: string, reason?: string) => {
      await apiRequest(`Booking/booking/id/${bookingId}/reject/staffId/${notifyStaffId}`, "PATCH", reason )
      .then((data) => {
        console.log("Booking rejected");
        console.log(data);
        if(reason) {
          bookingView.find(bv => bv.detailedBookingViewid === bookingId)!.staffNotes = reason;
        }
      })
      .catch((error) => {
        console.error("Error rejecting booking:", error);
      });
    };
    const confirmBooking = async (bookingId: string, notifyStaffId: string) => {
      await apiRequest(`Booking/booking/id/${bookingId}/confirm/staffId/${notifyStaffId}`, "PATCH")
      .then(() => {
        console.log("Booking confirmed for bookingId: " + bookingId);
      })
      .catch((error) => {
        console.error("Error confirming booking:", error);
      });
    };

    const markRead = async (notificationId: string) => {
    await fetch(`/api/staff/notifications/${notificationId}/read`, {
      method: "PATCH",
    });

    setNotifications((prev) =>
      prev.map((n) =>
        n.notifyStaffId === notificationId
          ? { ...n, notificationStatus: "read" }
          : n
      )
    );
  };

return (
    <div className="flex h-screen bg-gray-50">
      {/* LEFT ASIDE */}
      <aside className="w-64 border-r bg-white p-4 overflow-y-auto">
        <h2 className="text-xl font-semibold mb-4 text-gray-900">Notifications</h2>

        <NotificationList
          notifications={notifications}
          activeId={activeNotification?.notifyStaffId}
          onSelect={setActiveNotificationHandler}
        />
      </aside>

      {/* RIGHT MAIN CONTENT */}
      <main className="flex-1 p-6 overflow-y-auto">
        {activeNotification?.relatedBookingId ? (
          <BookingDetails
            booking={selectedBookingView}
            onConfirm={() => confirmBooking(activeNotification.relatedBookingId, activeNotification.staffId)}
            onReject={() => rejectBooking(activeNotification.relatedBookingId, activeNotification.staffId)}
          />
        ) : (
          <div className="text-gray-500 text-center mt-20">
            Select a notification to view booking details.
          </div>
        )}
      </main>
    </div>
  );
}