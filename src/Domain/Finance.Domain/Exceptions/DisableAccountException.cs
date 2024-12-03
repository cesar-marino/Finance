namespace Finance.Domain.Exceptions
{
    public class DisableUserException(
           string code = "disable-user",
           string? message = "Disable user",
           Exception? innerException = null) : DomainException(code, message, innerException)
    {
    }
}