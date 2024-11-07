
namespace Finance.Domain.Exceptions
{
    public class UsernameInUseException(
        string code = "username-in-use",
        string? message = "Username is already in use",
        Exception? innerException = null) : DomainException(code, message, innerException)
    {
    }
}