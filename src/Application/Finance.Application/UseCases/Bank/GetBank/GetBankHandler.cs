using Finance.Application.UseCases.Bank.Commons;

namespace Finance.Application.UseCases.Bank.GetBank
{
    public class GetBankHandler : IGetBankHandler
    {
        public Task<BankResponse> Handle(GetBankRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}