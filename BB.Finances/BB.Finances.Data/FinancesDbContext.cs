using BB.Finances.Data.DTO;
using BB.Finances.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BB.Finances.Data
{
    public class FinancesDbContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        public FinancesDbContext(DbContextOptions<FinancesDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeding currency values
            modelBuilder.Entity<Currency>()
                .HasData(
                new Currency()
                {
                    Id = Guid.NewGuid(),
                    Name = "US Dollar",
                    CurrencySign = "USD"
                },
                new Currency()
                {
                    Id = Guid.NewGuid(),
                    Name = "EURO",
                    CurrencySign = "EUR"
                },
                new Currency()
                {
                    Id = Guid.NewGuid(),
                    Name = "Russian rouble",
                    CurrencySign = "RUB"
                },
                new Currency()
                {
                    Id = Guid.NewGuid(),
                    Name = "Belarussian rouble",
                    CurrencySign = "BYN"
                });
        }
    }
}
