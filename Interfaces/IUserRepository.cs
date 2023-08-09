using ChatRooms.Models;

namespace ChatRooms.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetByIdAsync(int id);
        Task<User> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Chatroom>> GetAllJoinedChatrooms(int userId);

        bool Add(User user);
        bool Update(User user);
        bool Delete(User user);
        bool Save();
    }
}
