using ChatRooms.Models;

namespace ChatRooms.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(string id);
        bool Add(User user);
        bool Update(User user);
        bool Delete(User user);
        bool Save();

    }
}
