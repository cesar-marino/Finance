using Finance.Application.UseCases.Bank.Commons;

namespace Finance.Application.UseCases.Bank.CreateBank
{
    public class CreateBankHandler : ICreateBankHandler
    {
        public Task<BankResponse> Handle(CreateBankRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}