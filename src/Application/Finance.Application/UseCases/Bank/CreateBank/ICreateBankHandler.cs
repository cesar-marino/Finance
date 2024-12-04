using Finance.Application.UseCases.Bank.Commons;
using MediatR;

namespace Finance.Application.UseCases.Bank.CreateBank
{
    public interface ICreateBankHandler : IRequestHandler<CreateBankRequest, BankResponse>
    {

    }
}