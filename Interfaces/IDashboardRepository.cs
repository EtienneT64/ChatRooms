using ChatRooms.Models;

namespace ChatRooms.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Chatroom>> GetAllUserChatrooms();
        Task<List<Message>> GetAllUserMessages();
        Task<User> GetUserById(string id);
        Task<User> GetByIdNoTracking(string id);
        bool Update(User user);
        bool Save();

    }
}
