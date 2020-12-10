using Microsoft.EntityFrameworkCore;

namespace DatabaseOracleContext
{
    public class BaseDbContext : DbContext
    {
        private readonly string _connString;
        public BaseDbContext(string connstring)
        {
            _connString = connstring;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;
            optionsBuilder.UseOracle(_connString);
        }
    }
}
