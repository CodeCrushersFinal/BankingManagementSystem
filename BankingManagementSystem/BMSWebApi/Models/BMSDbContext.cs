using BMSWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BMSWebApi.Model
{
    public class BMSDbContext : DbContext
    {
        public BMSDbContext()
        {

        }
        public BMSDbContext(DbContextOptions<BMSDbContext> options) :base(options) 
        {

        }
          
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Loan> Loans { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Receiver> Receivers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=BMSDb;Integrated Security=True;TrustServerCertificate=True");

    }
}
