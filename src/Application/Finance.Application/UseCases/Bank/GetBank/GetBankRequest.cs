using Finance.Application.UseCases.Bank.Commons;
using MediatR;

namespace Finance.Application.UseCases.Bank.GetBank
{
    public class GetBankRequest(Guid bankId) : IRequest<BankResponse>
    {
        public Guid BankId { get; } = bankId;
    }
}