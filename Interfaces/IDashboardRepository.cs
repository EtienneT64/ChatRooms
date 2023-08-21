using ChatRooms.Models;

namespace ChatRooms.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Chatroom>> GetAllUserOwnedChatrooms();
        Task<List<Chatroom>> GetAllUserPinnedChatrooms();
        Task<User> GetUserById(string id);
        Task<User> GetByIdNoTracking(string id);
        bool Update(User user);
        bool Save();

    }
}
