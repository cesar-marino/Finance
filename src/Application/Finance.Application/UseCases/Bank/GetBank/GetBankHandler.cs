using Finance.Application.UseCases.Bank.Commons;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Bank.GetBank
{
    public class GetBankHandler(IBankRepository bankRepository) : IGetBankHandler
    {
        public async Task<BankResponse> Handle(GetBankRequest request, CancellationToken cancellationToken)
        {
            await bankRepository.FindAsync(bankId: request.BankId, cancellationToken);

            throw new NotImplementedException();
        }
    }
}