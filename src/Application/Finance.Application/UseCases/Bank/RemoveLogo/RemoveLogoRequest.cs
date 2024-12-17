using Finance.Application.UseCases.Bank.Commons;
using MediatR;

namespace Finance.Application.UseCases.Bank.RemoveLogo
{
    public class RemoveLogoRequest(Guid bankId) : IRequest<BankResponse>
    {
        public Guid BankId { get; } = bankId;
    }
}