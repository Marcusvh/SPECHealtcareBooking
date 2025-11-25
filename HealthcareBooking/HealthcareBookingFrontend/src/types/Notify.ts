export interface Notify {
    notifyStaffId: string;
    message: string;
    notificationStatus: string;
    notificationType: string;
    createdAt: string;
    relatedBookingId: string;
    relatedBooking: null | any;
    staffId: string;
    staff: null | any;    
}