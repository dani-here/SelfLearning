namespace SelfLearning.Common
{
    public class DomainError(string? errorMessage, ErrorType errorType)
    {
        public string? ErrorMessage { get; } = errorMessage;
        public ErrorType ErrorType { get; } = errorType;

        public static DomainError NotFound(string? message = "Resource not found") => new DomainError(message, ErrorType.NotFound);
        public static DomainError InvalidInput(string? message = "Invalid input") => new DomainError(message, ErrorType.InvalidInput);
    }

    public enum ErrorType
    {
        NotFound,
        InvalidInput
    }
}
