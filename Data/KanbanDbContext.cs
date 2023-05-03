using KanbanAPI.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Data
{
    public class KanbanDbContext : IdentityDbContext<KanbanUser>
    {
        public KanbanDbContext(DbContextOptions<KanbanDbContext> options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KanbanUser>(u =>
            {
                u.HasKey(u => u.Id);
                u.ToTable("Users");
            });
            modelBuilder.Entity<KanbanUserLogin>(ul =>
            {
                ul.HasKey(u => u.UserId);
                ul.ToTable("UserLogins");
            });

            modelBuilder.Entity<Client>().HasKey(c => c.ClientId);
            modelBuilder.Entity<Client>().HasMany(c => c.Projects).WithOne(p => p.Client);

            modelBuilder.Entity<Project>().HasKey(p => p.ProjectId);
            modelBuilder.Entity<Project>().HasMany(p => p.Tasks).WithOne(t => t.Project);
            modelBuilder.Entity<Project>().HasOne(p => p.Client).WithMany(c => c.Projects).HasForeignKey(p => p.ClientId);

            modelBuilder.Entity<TaskItem>().HasKey(t => t.TaskId);
            modelBuilder.Entity<TaskItem>().HasOne(t => t.Project).WithMany(p => p.Tasks).HasForeignKey(t => t.ProjectId);
            modelBuilder.Entity<TaskItem>().HasOne(t => t.Client).WithMany(c => c.Tasks).HasForeignKey(t => t.ClientId);
        }
    }
}
