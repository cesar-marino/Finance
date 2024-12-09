using Finance.Application.Services;
using Finance.Application.UseCases.Bank.Commons;

namespace Finance.Application.UseCases.Bank.CreateBank
{
    public class CreateBankHandler(IStorageService storageService) : ICreateBankHandler
    {
        public async Task<BankResponse> Handle(CreateBankRequest request, CancellationToken cancellationToken)
        {
            if (request.Logo is not null)
                await storageService.UploadAsync("/bank", request.Logo, cancellationToken);

            throw new NotImplementedException();
        }
    }
}