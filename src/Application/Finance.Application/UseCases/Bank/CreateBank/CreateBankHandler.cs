using Finance.Application.UseCases.Bank.Commons;
using Finance.Domain.Entities;
using Finance.Domain.Repositories;

namespace Finance.Application.UseCases.Bank.CreateBank
{
    public class CreateBankHandler(IBankRepository bankRepository) : ICreateBankHandler
    {
        public async Task<BankResponse> Handle(CreateBankRequest request, CancellationToken cancellationToken)
        {
            var bank = new BankEntity(code: request.Code, name: request.Name, color: request.Color, logo: request.Logo);
            await bankRepository.InsertAsync(bank, cancellationToken);

            throw new NotImplementedException();
        }
    }
}