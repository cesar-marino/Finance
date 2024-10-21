namespace Finance.Domain.Exceptions
{
    public class NotFoundException(
        string entity,
        string code = "not-found",
        Exception? innerException = null) : DomainException(code, $"{entity} not found", innerException)
    {
    }
}
