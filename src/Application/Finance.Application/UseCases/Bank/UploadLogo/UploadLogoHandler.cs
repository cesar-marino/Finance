using Finance.Application.UseCases.Bank.Commons;

namespace Finance.Application.UseCases.Bank.UploadLogo
{
    public class UploadLogoHandler : IUploadLogoHandler
    {
        public Task<BankResponse> Handle(UploadLogoRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}