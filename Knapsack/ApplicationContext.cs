using Microsoft.EntityFrameworkCore;

namespace Knapsack
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Task> tasks { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.\AYYASQL; Initial Catalog=KnapsackDB; Integrated Security=True");
        }
    }
}