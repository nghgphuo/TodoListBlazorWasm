using Microsoft.AspNetCore.Identity;
using TodoList.API.Entities;
using TodoList.Models.Enums;
using Task = System.Threading.Tasks.Task;

namespace TodoList.API.Data
{
    public class TodoListDbContextSeed
    {
        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public async Task SeedAsync(TodoListDbContext context, ILogger<TodoListDbContextSeed> logger)
        {
            if(!context.Users.Any())
            {
                var user = new User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Mr. ",
                    LastName = "A",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN1@GMAIL.COM",
                    PhoneNumber = "0123456789",
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                user.PasswordHash = _passwordHasher.HashPassword(user, "Admin@123$");
                context.Users.Add(user);
            }

            if(!context.Tasks.Any())
            {
                context.Tasks.Add(new Entities.Task()
                {
                    Id= Guid.NewGuid(),
                    Name = "Same tasks 1",
                    CreatedDate = DateTime.Now,
                    Priority = Priority.High,
                    Status = Status.Open
                });
            }
            await context.SaveChangesAsync();
        }
    }
}
