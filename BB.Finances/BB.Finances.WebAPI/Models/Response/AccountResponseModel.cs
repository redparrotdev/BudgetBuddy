namespace BB.Finances.WebAPI.Models.Response
{
    public class AccountResponseModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public double Balance { get; set; }

        public DateTime CreationDate { get; set; }

        public string Currency { get; set; }
    }
}
