using Finance.Application.UseCases.Bank.Commons;

namespace Finance.Application.UseCases.Bank.EnableBank
{
    public class EnableBankHandler : IEnableBankHandler
    {
        public Task<BankResponse> Handle(EnableBankRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}