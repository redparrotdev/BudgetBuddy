using BB.Finances.Data.Entities;
using BB.Finances.WebAPI.Extentions;
using BB.Finances.WebAPI.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Finances.Tests.Fixtures
{
    public static class AccountFixtures
    {
        private static readonly Random _random = new Random();

        private static Currency GetRandomCurrency() => _random.Next(1, 4) switch
        {
            1 => Currency.RUB,
            2 => Currency.BYN,
            3 => Currency.USD,
            4 => Currency.EUR,
            _ => Currency.USD
        };

        public static AccountRequestModel GetRequestFixture() => new AccountRequestModel
        {
            UserId = Guid.NewGuid(),
            Name = "Fixture Account",
            Balance = _random.Next(10, 10000),
            Currency = GetRandomCurrency().CurrencyToString()
        };

        public static Account GetEntityFixture() => new Account()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "FixtureAccount",
            Balance = _random.Next(10, 10000),
            CreationDate = DateTime.Now,
            Currency = GetRandomCurrency(),
            IsDeleted = false
        };

        public static IEnumerable<Account> GetEntityFixturesList(int count = 5)
        {
            if (count <= 0)
                count = 1;

            List<Account> accounts = new List<Account>();

            for (int i = 0; i < count; i++)
            {
                accounts.Add(AccountFixtures.GetEntityFixture());
            }

            return accounts;
        }
    }
}
