namespace Finance.Domain.Exceptions
{
    public class UnexpectedException(
        string? message = "An unexpected error occurred",
        Exception? innerException = null) : DomainException("unexpected", message, innerException)
    {
    }
}
