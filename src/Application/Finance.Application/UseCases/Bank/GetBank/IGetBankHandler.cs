using Finance.Application.UseCases.Bank.Commons;
using MediatR;

namespace Finance.Application.UseCases.Bank.GetBank
{
    public interface IGetBankHandler : IRequestHandler<GetBankRequest, BankResponse>
    {

    }
}