using bankAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace bankAPI.Context
{
    public class applicationDbContext : DbContext
    {

        public applicationDbContext(DbContextOptions<applicationDbContext> options) : base(options) { }

        public DbSet<Customer> customers { get; set; }
        //public DbSet<kycDetails> kyc { get; set; }

    }
}
