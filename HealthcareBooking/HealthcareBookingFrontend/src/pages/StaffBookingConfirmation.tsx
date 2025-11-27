import { useState, useEffect } from "react";
import { apiRequest } from "../apiRequest/HealthcareApi";
import { type Notify } from "../types/Notify";
import NotificationList from "../components/NotificationList";
import BookingDetails from "../components/BookingDetails";
import type { BookingView } from "../types/bookingView";
import BookingList from "../components/BookingList";

export const StaffBookingConfirmation: React.FC = () => {
    const [bookingViewNeedAction, setBookingViewNeedAction] = useState<BookingView[]>([]);
    const [bookingViewAll, setBookingViewAll] = useState<BookingView[]>([]);
    const [selectedBookingView, setSelectedBookingView] = useState<BookingView>();
    const [notifications, setNotifications] = useState<Notify[]>([]);
    const [activeNotification, setActiveNotification] = useState<Notify | null>(null);
    const [viewMode, setViewMode] = useState<"notifications" | "bookings">("notifications");

    useEffect(() => {
        // Fetch bookings
        apiRequest("Booking/booking/detailedViews", "GET").then((data) => {
            console.log(data);
            setBookingViewNeedAction(data);
            setBookingViewAll(data);
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
      const relatedBooking = bookingViewNeedAction.find(
        (bv) => bv.detailedBookingViewid === activeNotification.relatedBookingId
      );
      if (relatedBooking) {
        setSelectedBookingView(relatedBooking);
      }
    }}, [activeNotification, bookingViewNeedAction]);

    // Handlers
    const setActiveNotificationHandler = (notification: Notify) => {
      markRead(notification.notifyStaffId);
      if(activeNotification?.notifyStaffId === notification.notifyStaffId) setActiveNotification(null);
      else setActiveNotification(notification);
    }
    const setSelectedBookingViewHandler = (bv: BookingView) => {
      if(selectedBookingView?.detailedBookingViewid === bv.detailedBookingViewid) setSelectedBookingView(undefined);
      else setSelectedBookingView(bv);
    }
    const setViewModeHandler = (mode: "notifications" | "bookings") => {
      setViewMode(mode);
      setActiveNotification(null);
      setSelectedBookingView(undefined);
    }

    // Actions
    const rejectBooking = async (bookingId: string, notifyStaffId: string, reason?: string) => {
      await apiRequest(`Booking/booking/id/${bookingId}/reject/staffId/${notifyStaffId}`, "PATCH", reason )
      .then(() => {
        console.log("Booking rejected");
        if(reason) {
          bookingViewNeedAction.find(bv => bv.detailedBookingViewid === bookingId)!.staffNotes = reason;
        }
      })
      .catch((error) => {
        console.error("Error rejecting booking:", error);
      });
    };
    const confirmBooking = async (bookingId: string, notifyStaffId: string) => {
      await apiRequest(`Booking/booking/id/${bookingId}/confirm/staffId/${notifyStaffId}`, "PATCH")
      .then(() => {
        
      })
      .catch((error) => {
        console.error("Error confirming booking:", error);
      });
    };

    const markRead = async (notificationId: string) => {
      await apiRequest(`Notify/staff/id/${notificationId}/read`, "PATCH");

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
        <button onClick={() => setViewModeHandler(viewMode === "notifications" ? "bookings" : "notifications")} className="w-full bg-rose-600 text-white py-2 px-4 rounded mb-4">
          {viewMode === "notifications" ? "Show Bookings" : "Show Notifications"}
        </button>
        {viewMode === "notifications" ? (
        <div>
          <h2 className="text-xl font-semibold mb-4 text-gray-900">Notifications</h2>
          <NotificationList
          notifications={notifications}
          activeId={activeNotification?.notifyStaffId}
          onSelect={setActiveNotificationHandler}
          />
        </div>
        ) : (
          <div>
            <h2 className="text-xl font-semibold mb-4 text-gray-900">Bookings</h2>
            <BookingList
              bookings={bookingViewAll}
              activeId={selectedBookingView?.detailedBookingViewid}
              onSelect={(bv) => setSelectedBookingViewHandler(bv)}
            />
          </div>
        )}
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
          selectedBookingView ? (
            <BookingDetails
              booking={selectedBookingView}
              needsAction={false}
            />
          ) : (
          <div className="text-gray-500 text-center mt-20">
            Select a notification to view booking details.
          </div>
        ))}
      </main>
    </div>
  );
}