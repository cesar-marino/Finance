using Finance.Application.UseCases.Bank.Commons;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Bank.UpdateBank
{
    public class UpdateBankHandler(
        IBankRepository bankRepository,
        IUnitOfWork unitOfWork) : IUpdateBankHandler
    {
        public async Task<BankResponse> Handle(UpdateBankRequest request, CancellationToken cancellationToken)
        {
            var bank = await bankRepository.FindAsync(id: request.BankId, cancellationToken: cancellationToken);

            bank.Update(
                name: request.Name,
                code: request.Code,
                color: request.Color);

            await bankRepository.UpdateAsync(bank, cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return BankResponse.FromEntity(bank);
        }
    }
}