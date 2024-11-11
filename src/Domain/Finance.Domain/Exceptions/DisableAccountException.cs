namespace Finance.Domain.Exceptions
{
    public class DisableAccountException(
           string code = "disable-account",
           string? message = "Disable account",
           Exception? innerException = null) : DomainException(code, message, innerException)
    {
    }
}