using Finance.Application.UseCases.Bank.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Bank.DisableBank
{
    public class DisableBankHandler(IBankRepository bankRepository) : IDisableBankHandler
    {
        public async Task<BankResponse> Handle(DisableBankRequest request, CancellationToken cancellationToken)
        {
            await bankRepository.FindAsync(request.BankId, cancellationToken);

            throw new NotImplementedException();
        }
    }
}