using Finance.Application.UseCases.Bank.Commons;
using MediatR;

namespace Finance.Application.UseCases.Bank.EnableBank
{
    public interface IEnableBankHandler : IRequestHandler<EnableBankRequest, BankResponse>
    {

    }
}