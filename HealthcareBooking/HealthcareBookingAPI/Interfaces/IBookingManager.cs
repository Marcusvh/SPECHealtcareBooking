using HealthcareBookingAPI.DTO;
using HealthcareModels.Models;

namespace HealthcareBookingAPI.Interfaces
{
    public interface IBookingManager
    {
        Task<List<BookingType>> GetAllBookingTypesAsync();
        Task<BookingType?> GetBookingTypeByIdAsync(Guid id);
        Task<ResultResponse<BookingType>> GetBookingTypeByNameAsync(string name);
        Task<List<Booking>> GetAllBookingsAsync();
        Task<Booking?> GetBookingByIdAsync(Guid id);
        Task<ResultResponse<Booking>> CreateBookingAsync(BookingDTO booking);
        Task<List<Booking>> GetBookingsByStageAsync(BookingCheckStage stage);
        Task<List<DetailedBookingViewDTO>> GetDetailedBookingViewsAsync();
        Task<ResultResponse<Guid>> UpdateStaffNoteOnBooking(Guid bookingId, string staffNote);
        Task<ResultResponse<Guid>> ConfirmBookingByStaffAsync(Guid bookingId, Guid staffId);
        Task<ResultResponse<Guid>> RejectBookingByStaffAsync(Guid bookingId, Guid staffId, string? reason);
    }
}
