namespace Finance.Application.Services
{
    public interface IAccountService
    {
        Task CreateAsync(Guid accountId, string email, string username, string passwrd);
    }
}