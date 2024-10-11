
namespace Finance.Domain.Exceptions
{
    public class NotFoundException(
        string entity,
        Exception? innerException = null) : DomainException("not-found", $"{entity} not found", innerException)
    {
    }
}
