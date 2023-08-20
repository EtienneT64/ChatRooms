using ChatRooms.Models;

namespace ChatRooms.Interfaces
{
    public interface IChatroomRepository
    {
        Task<IEnumerable<Chatroom>> GetAll();
        Task<Chatroom> GetByIdAsync(int? id);
        Task<Chatroom> GetByIdAsyncNoTracking(int? id);
        Task<Chatroom> GetByNameAsync(string? chatroomName);
        Task<Chatroom> GetByNameAsyncNoTracking(string? chatroomName);

        bool Add(Chatroom chatroom);
        bool Update(Chatroom chatroom);
        bool Delete(Chatroom chatroom);
        bool Save();

    }
}
