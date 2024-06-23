using BB.Finances.Data.Entities;

namespace BB.Finances.WebAPI.Extentions
{
    public static class CurrencyExtention
    {
        public static string CurrencyToString(this Currency currency) => currency.ToString().ToUpperInvariant();

        public static Currency CurrencyStringToCurrencyEnum(this string currencyString) => (Currency) Enum.Parse(typeof(Currency), currencyString, true);
    }
}
