namespace BB.Finances.WebAPI.Models.Response
{
    public class ExpenseResponseModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public double ValueOut { get; set; }

        public double ValueIn { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public Guid AccountId { get; set; }

        public Guid CategoryId { get; set; }
    }
}
