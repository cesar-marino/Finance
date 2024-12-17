using Finance.Application.UseCases.Bank.Commons;

namespace Finance.Application.UseCases.Bank.UpdateBank
{
    public class UpdateBankHandler : IUpdateBankHandler
    {
        public Task<BankResponse> Handle(UpdateBankRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}