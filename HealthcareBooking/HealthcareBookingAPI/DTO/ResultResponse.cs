namespace HealthcareBookingAPI.DTO
{
    public class ResultResponse<T>
    {
        public bool IsSuccess { get; }
        public string? Error { get; }
        public T? Value { get; }

        public static ResultResponse<T> Success(T value) => new(true, null, value);
        public static ResultResponse<T> Fail(string error) => new(false, error, default);

        private ResultResponse(bool success, string? error, T? value)
        {
            IsSuccess = success;
            Error = error;
            Value = value;
        }
    }

}
