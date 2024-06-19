namespace BB.Finances.WebAPI.Models.Response
{
    public class AccountResponseModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public double Balance { get; set; }

        public DateTime creationDate { get; set; }

        public Guid CurrencyId { get; set; }

        public string CurrencySign { get; set; }
    }
}
