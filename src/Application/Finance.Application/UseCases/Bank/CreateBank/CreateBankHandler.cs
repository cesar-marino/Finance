using Finance.Application.UseCases.Bank.Commons;
using Finance.Domain.Entities;
using Finance.Domain.Repositories;
using Finance.Domain.SeedWork;

namespace Finance.Application.UseCases.Bank.CreateBank
{
    public class CreateBankHandler(
        IBankRepository bankRepository,
        IUnitOfWork unitOfWork) : ICreateBankHandler
    {
        public async Task<BankResponse> Handle(CreateBankRequest request, CancellationToken cancellationToken)
        {
            var bank = new BankEntity(
                code: request.Code,
                name: request.Name,
                color: request.Code);

            await bankRepository.InsertAsync(
                aggregate: bank,
                cancellationToken: cancellationToken);

            await unitOfWork.CommitAsync(cancellationToken: cancellationToken);

            throw new NotImplementedException();
        }
    }
}