using Finance.Application.UseCases.Bank.Commons;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Bank.DisableBank
{
    public class DisableBankHandler(
        IBankRepository bankRepository,
        IUnitOfWork unitOfWork) : IDisableBankHandler
    {
        public async Task<BankResponse> Handle(DisableBankRequest request, CancellationToken cancellationToken)
        {
            var bank = await bankRepository.FindAsync(request.BankId, cancellationToken);
            bank.Disable();

            await bankRepository.UpdateAsync(bank, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return BankResponse.FromEntity(bank);
        }
    }
}