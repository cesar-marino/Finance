using Finance.Application.UseCases.Bank.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Bank.UpdateBank
{
    public class UpdateBankHandler(IBankRepository bankRepository) : IUpdateBankHandler
    {
        public async Task<BankResponse> Handle(UpdateBankRequest request, CancellationToken cancellationToken)
        {
            var bank = await bankRepository.FindAsync(id: request.BankId, cancellationToken: cancellationToken);

            await bankRepository.UpdateAsync(aggregate: bank, cancellationToken: cancellationToken);

            throw new NotImplementedException();
        }
    }
}