using Finance.Application.UseCases.Bank.Commons;
using MediatR;

namespace Finance.Application.UseCases.Bank.CreateBank
{
    public class CreateBankRequest(
        string? code,
        string name,
        string? color,
        byte[]? logo) : IRequest<BankResponse>
    {
        public string? Code { get; } = code;
        public string Name { get; } = name;
        public string? Color { get; } = color;
        public byte[]? Logo { get; } = logo;
    }
}