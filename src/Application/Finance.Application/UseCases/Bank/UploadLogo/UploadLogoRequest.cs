using Finance.Application.UseCases.Bank.Commons;
using MediatR;

namespace Finance.Application.UseCases.Bank.UploadLogo
{
    public class UploadLogoRequest(
        Guid bankId,
        Stream logo) : IRequest<BankResponse>
    {
        public Guid BankId { get; } = bankId;
        public Stream Logo { get; } = logo;
    }
}