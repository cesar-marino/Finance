using Finance.Application.UseCases.Bank.Commons;
using MediatR;

namespace Finance.Application.UseCases.Bank.CreateBank
{
    public class CreateBankRequest(
        string name,
        string? code,
        string? color) : IRequest<BankResponse>
    {
        public string Name { get; } = name;
        public string? Code { get; } = code;
        public string? Color { get; } = color;
    }
}