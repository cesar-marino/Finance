
namespace Finance.Domain.Exceptions
{
    public class UnexpectedException(
        string code = "unexpected",
        string? message = "An unexpected error occurred",
        Exception? innerException = null) : DomainException(code, message, innerException)
    {
    }
}
