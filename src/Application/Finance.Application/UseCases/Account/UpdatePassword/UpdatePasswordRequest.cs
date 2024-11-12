using Finance.Application.UseCases.Account.Commons;
using MediatR;

namespace Finance.Application.UseCases.Account.UpdatePassword
{
    public class UpdatePasswordRequest(
        Guid accountId,
        string currentPassword,
        string newPassword) : IRequest<AccountResponse>
    {
        public Guid AccountId { get; } = accountId;
        public string CurrentPassword { get; } = currentPassword;
        public string NewPassword { get; } = newPassword;
    }
}