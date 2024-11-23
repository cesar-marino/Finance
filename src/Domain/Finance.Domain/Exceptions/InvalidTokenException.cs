
namespace Finance.Domain.Exceptions
{
    public class InvalidTokenException(
        string code = "invalid-token",
        string? message = "Token is invalid",
        Exception? innerException = null) : DomainException(code, message, innerException)
    {
    }
}