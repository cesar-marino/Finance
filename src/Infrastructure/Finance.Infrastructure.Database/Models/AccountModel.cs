namespace Finance.Infrastructure.Database.Models
{
    public class AccountModel
    {
        public Guid AccountId { get; set; }

        public List<TagModel>? Tags { get; set; }
    }
}
