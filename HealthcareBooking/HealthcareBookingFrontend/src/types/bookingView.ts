export interface BookingView {
    detailedBookingViewid: string;
    patientFullName: string;
    dob: string;
    bookingStatus: string;
    patientNotes?: string;
    staffNotes?: string;
    startTime: string;
    bookingTypeName: string;
    duration: string;
    patientEmail: string;
    patientPhoneNumber: string;
}