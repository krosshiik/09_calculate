using Microsoft.EntityFrameworkCore;

namespace _09_Calculate.Data
{
    public class CalculatorContext : DbContext
    {
        public DbSet<DataInputVariant> DataInputVariants { get; set; }

        public CalculatorContext(DbContextOptions<CalculatorContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Optionally add any model configurations here in the future.
        }
    }
}
