using Finance.Application.UseCases.Bank.Commons;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Bank.EnableBank
{
    public class EnableBankHandler(
        IBankRepository bankRepository,
        IUnitOfWork unitOfWork) : IEnableBankHandler
    {
        public async Task<BankResponse> Handle(EnableBankRequest request, CancellationToken cancellationToken)
        {
            var bank = await bankRepository.FindAsync(request.BankId, cancellationToken);
            await bankRepository.UpdateAsync(bank, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            throw new NotImplementedException();
        }
    }
}