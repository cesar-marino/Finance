namespace Finance.Domain.Exceptions
{
    public class UnauthorizedException(
        string code = "unauthorized",
        string? message = "Unauthorized access",
        Exception? innerException = null) : DomainException(code, message, innerException)
    {
    }
}