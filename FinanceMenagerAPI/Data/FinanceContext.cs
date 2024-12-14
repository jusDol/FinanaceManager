using FinanceMenagerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceMenagerAPI.Data
{
    public class FinanceContext : DbContext
    {
        public FinanceContext(DbContextOptions<FinanceContext> options) : base(options) { }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
