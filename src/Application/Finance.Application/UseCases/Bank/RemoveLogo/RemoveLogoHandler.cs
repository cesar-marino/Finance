using Finance.Application.UseCases.Bank.Commons;

namespace Finance.Application.UseCases.Bank.RemoveLogo
{
    public class RemoveLogoHandler : IRemoveLogoHandler
    {
        public Task<BankResponse> Handle(RemoveLogoRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}