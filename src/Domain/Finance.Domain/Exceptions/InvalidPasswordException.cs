namespace Finance.Domain.Exceptions
{
    public class InvalidPasswordException(
            string code = "invalid-password",
            string? message = "Incorrect password",
            Exception? innerException = null) : DomainException(code, message, innerException)
    {
    }
}