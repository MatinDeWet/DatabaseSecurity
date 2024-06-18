using DatabaseSecurity.UnitTests.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseSecurity.UnitTests.Context
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TestContext> options) : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserTeam> UserTeams { get; set; }

        public virtual DbSet<Team> Teams { get; set; }

        public virtual DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(TestContext).Assembly);

            base.OnModelCreating(builder);
        }
    }
}
