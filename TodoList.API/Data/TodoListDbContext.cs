using Microsoft.EntityFrameworkCore;
using Task = TodoList.API.Entities.Task;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TodoList.API.Entities;
using TodoList.Models.Enums;

namespace TodoList.API.Data
{
    public class TodoListDbContext : IdentityDbContext<User, Role, Guid>
    {
        public TodoListDbContext(DbContextOptions<TodoListDbContext> options) : base(options)
        {
            
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring Task entity
            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.CreatedDate);

                entity.HasOne(e => e.Assignee)
                    .WithMany()
                    .HasForeignKey(e => e.AssigneeId)
                    .OnDelete(DeleteBehavior.SetNull) // Or another delete behavior
                    .IsRequired(false);

                entity.Property(e => e.Priority)
                    .HasConversion(
                        v => v.ToString(),
                        v => (Priority)Enum.Parse(typeof(Priority), v))
                    .IsRequired();

                entity.Property(e => e.Status)
                    .HasConversion(
                        v => v.ToString(),
                        v => (Status)Enum.Parse(typeof(Status), v))
                    .IsRequired();
            });

            // Configuring Role entity
            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            // Configuring User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(250);
            });
        }
    }
}
