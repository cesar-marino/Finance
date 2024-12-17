using Finance.Application.UseCases.Bank.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Bank.GetBank
{
    public class GetBankHandler(IBankRepository bankRepository) : IGetBankHandler
    {
        public async Task<BankResponse> Handle(GetBankRequest request, CancellationToken cancellationToken)
        {
            var bank = await bankRepository.FindAsync(request.BankId, cancellationToken);
            return BankResponse.FromEntity(bank);
        }
    }
}