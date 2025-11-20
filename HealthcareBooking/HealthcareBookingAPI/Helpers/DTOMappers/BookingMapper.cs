using HealthcareBookingAPI.DTO;
using HealthcareModels.Models;

namespace HealthcareBookingAPI.Helpers.DTOMappers
{
    public static class BookingMapper
    {
        public static Booking MapBooking(BookingDTO dto)
        {
            Booking booking = new Booking()
            {
                BookingTypeId = dto.BookingTypeId,
                StartTime = dto.StartTime,
                PatientId = dto.PatientId,
                PatientNotes = dto.PatientNotes
            };
            return booking;
        }
    }
}
