using HWP_backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HWP_backend.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<SolvedTask> SolvedTasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SolvedTask>()
                .HasKey(ut => new {ut.UserId, ut.TaskId});
            modelBuilder.Entity<SolvedTask>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.SolvedTasks)
                .HasForeignKey(ut => ut.UserId);
            modelBuilder.Entity<SolvedTask>()
                .HasOne(ut => ut.Task)
                .WithMany(t => t.SolvedTasks)
                .HasForeignKey(ut => ut.TaskId);
        }
    }
}