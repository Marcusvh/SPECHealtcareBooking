export interface Booking {
    startTime: Date,
    patientNotes?: string,
    bookingTypeId: string,
    patientId: string,
    staffId?: string
}
