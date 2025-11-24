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
        Task<ResultResponse<Guid>> CreateBookingAsync(BookingDTO booking);
    }
}
