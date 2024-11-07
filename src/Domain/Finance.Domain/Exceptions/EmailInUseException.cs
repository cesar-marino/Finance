
namespace Finance.Domain.Exceptions
{
    public class EmailInUseException(
        string code = "email-in-use",
        string? message = "Email is already in use",
        Exception? innerException = null) : DomainException(code, message, innerException)
    {
    }
}