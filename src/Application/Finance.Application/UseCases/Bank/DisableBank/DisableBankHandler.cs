using Finance.Application.UseCases.Bank.Commons;

namespace Finance.Application.UseCases.Bank.DisableBank
{
    public class DisableBankHandler : IDisableBankHandler
    {
        public Task<BankResponse> Handle(DisableBankRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}