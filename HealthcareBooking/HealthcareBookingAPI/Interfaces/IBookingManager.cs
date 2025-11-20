using HealthcareBookingAPI.DTO;
using HealthcareModels.Models;

namespace HealthcareBookingAPI.Interfaces
{
    public interface IBookingManager
    {
        Task<List<BookingType>> GetAllBookingTypesAsync();
        Task<BookingType> GetBookingTypeByIdAsync(Guid id);
        Task<BookingType> GetBookingTypeByNameAsync(string name);
        Task<List<Booking>> GetAllBookingsAsync();
        Task<Booking> GetBookingByIdAsync(Guid id);
        Task<string> CreateBooking(BookingDTO booking);
    }
}
