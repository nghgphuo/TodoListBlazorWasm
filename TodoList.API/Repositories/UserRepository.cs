using Microsoft.EntityFrameworkCore;
using TodoList.API.Data;
using TodoList.API.Entities;

namespace TodoList.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TodoListDbContext _context;

        public UserRepository(TodoListDbContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetUserList()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
