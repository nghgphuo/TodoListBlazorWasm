using TodoList.API.Entities;

namespace TodoList.API.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUserList();
    }
}
