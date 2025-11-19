namespace HealthcareBookingAPI.DTO
{
    public record DataGenerationBookingsDTO
    {
        public int NumBookingType { get; init; }
        public int NumBooking { get; init; }
    }
    public record DataGenerationStaffPatientsLocationsDTO
    {
        public int NumLocation { get; init; }
        public int NumPatient { get; init; }
        public Guid? FixedLocationId { get; init; }
        public int NumStaff { get; init; }
        public int NumDoctor { get; init; }
        public int NumNurse { get; init; }
        public int NumMedStudent { get; init; }
    }
    public record DataGenerationAllDTO
    {
        public int NumLocation { get; init; }
        public int NumPatient { get; init; }
        public Guid? FixedLocationId { get; init; }
        public int NumStaff { get; init; }
        public int NumDoctor { get; init; }
        public int NumNurse { get; init; }
        public int NumMedStudent { get; init; }
        public int NumBookingType { get; init; }
        public int NumBooking { get; init; }
    }
}
