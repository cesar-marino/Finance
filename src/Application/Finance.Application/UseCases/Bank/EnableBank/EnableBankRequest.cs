using Finance.Application.UseCases.Bank.Commons;
using MediatR;

namespace Finance.Application.UseCases.Bank.EnableBank
{
    public class EnableBankRequest(Guid bankId) : IRequest<BankResponse>
    {
        public Guid BankId { get; } = bankId;
    }
}