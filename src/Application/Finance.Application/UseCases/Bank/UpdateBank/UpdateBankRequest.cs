using Finance.Application.UseCases.Bank.Commons;
using MediatR;

namespace Finance.Application.UseCases.Bank.UpdateBank
{
    public class UpdateBankRequest(
        Guid bankId,
        string name,
        string? code,
        string? color) : IRequest<BankResponse>
    {
        public Guid BankId { get; } = bankId;
        public string Name { get; } = name;
        public string? Code { get; } = code;
        public string? Color { get; } = color;
    }
}