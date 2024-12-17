using Finance.Application.UseCases.Bank.Commons;
using MediatR;

namespace Finance.Application.UseCases.Bank.DisableBank
{
    public interface IDisableBankHandler : IRequestHandler<DisableBankRequest, BankResponse>
    {

    }
}