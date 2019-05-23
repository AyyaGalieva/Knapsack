using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Knapsack
{
    public class ApplicationContext : DbContext
    {
        public Microsoft.EntityFrameworkCore.DbSet<Task> Tasks { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Details> Details { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Item> Items { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<ExecutionProcess> ExecutionProcesses { get; set; }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }
        
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>()
                .HasKey(ti => new { ti.TaskId, ti.ItemId });
 
            modelBuilder.Entity<TaskItem>()
                .HasOne(ti => ti.Task)
                .WithMany(t => t.TaskItems)
                .HasForeignKey(ti => ti.TaskId);
 
            modelBuilder.Entity<TaskItem>()
                .HasOne(ti => ti.Item)
                .WithMany(i => i.TaskItems)
                .HasForeignKey(ti => ti.ItemId);
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.\AYYASQL; Initial Catalog=KnapsackDB; Integrated Security=True");
        }
    }
}