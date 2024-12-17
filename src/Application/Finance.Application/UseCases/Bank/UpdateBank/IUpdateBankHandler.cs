using Finance.Application.UseCases.Bank.Commons;
using MediatR;

namespace Finance.Application.UseCases.Bank.UpdateBank
{
    public interface IUpdateBankHandler : IRequestHandler<UpdateBankRequest, BankResponse>
    {

    }
}