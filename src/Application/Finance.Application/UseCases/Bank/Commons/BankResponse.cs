using Finance.Domain.Entities;

namespace Finance.Application.UseCases.Bank.Commons
{
    public class BankResponse(
        Guid bankId,
        bool active,
        string name,
        string? code,
        string? color,
        string? logo,
        DateTime createdAt,
        DateTime updatedAt)
    {
        public Guid BankId { get; } = bankId;
        public bool Active { get; } = active;
        public string Name { get; } = name;
        public string? Code { get; } = code;
        public string? Color { get; } = color;
        public string? Logo { get; } = logo;
        public DateTime CreatedAt { get; } = createdAt;
        public DateTime UpdatedAt { get; } = updatedAt;

        public static BankResponse FromEntity(BankEntity bank) => new(
            bankId: bank.Id,
            active: bank.Active,
            name: bank.Name,
            code: bank.Code,
            color: bank.Color,
            logo: bank.Logo,
            createdAt: bank.CreatedAt,
            updatedAt: bank.UpdatedAt);
    }
}