using Finance.Application.UseCases.Bank.Commons;
using MediatR;

namespace Finance.Application.UseCases.Bank.RemoveLogo
{
    public interface IRemoveLogoHandler : IRequestHandler<RemoveLogoRequest, BankResponse>
    {

    }
}