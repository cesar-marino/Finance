using Finance.Application.UseCases.Bank.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Bank.EnableBank
{
    public class EnableBankHandler(IBankRepository bankRepository) : IEnableBankHandler
    {
        public async Task<BankResponse> Handle(EnableBankRequest request, CancellationToken cancellationToken)
        {
            var bank = await bankRepository.FindAsync(request.BankId, cancellationToken);
            await bankRepository.UpdateAsync(bank, cancellationToken);
            throw new NotImplementedException();
        }
    }
}