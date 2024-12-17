using Finance.Application.UseCases.Bank.Commons;
using MediatR;

namespace Finance.Application.UseCases.Bank.UploadLogo
{
    public interface IUploadLogoHandler : IRequestHandler<UploadLogoRequest, BankResponse>
    {

    }
}