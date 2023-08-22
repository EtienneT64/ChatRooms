using ChatRooms.Models;

namespace ChatRooms.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserByIdAsyncNoTracking(string? id);

        int CountMessagesByUserId(string id);
        int CountChatroomsByUserId(string id);
        int CountPinnedChatroomsByUserId(string id);

        bool Add(User user);
        bool Update(User user);
        bool Delete(User user);
        bool Save();

    }
}
