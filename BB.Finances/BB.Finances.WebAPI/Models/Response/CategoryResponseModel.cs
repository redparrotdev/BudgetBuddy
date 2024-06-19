namespace BB.Finances.WebAPI.Models.Response
{
    public class CategoryResponseModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public Guid CurrencyId { get; set; }

        public string CurrencySign { get; set; }
    }
}
