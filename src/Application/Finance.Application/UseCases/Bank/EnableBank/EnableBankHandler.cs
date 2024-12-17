using Finance.Application.UseCases.Bank.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Bank.EnableBank
{
    public class EnableBankHandler(IBankRepository bankRepository) : IEnableBankHandler
    {
        public async Task<BankResponse> Handle(EnableBankRequest request, CancellationToken cancellationToken)
        {
            await bankRepository.FindAsync(request.BankId, cancellationToken);
            throw new NotImplementedException();
        }
    }
}