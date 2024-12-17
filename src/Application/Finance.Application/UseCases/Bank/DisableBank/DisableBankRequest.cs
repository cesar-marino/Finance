using Finance.Application.UseCases.Bank.Commons;
using MediatR;

namespace Finance.Application.UseCases.Bank.DisableBank
{
    public class DisableBankRequest(Guid bankId) : IRequest<BankResponse>
    {
        public Guid BankId { get; } = bankId;
    }
}