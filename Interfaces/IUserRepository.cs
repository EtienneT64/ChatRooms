using ChatRooms.Models;

namespace ChatRooms.Interfaces
{
    public interface IUserRepository
    {

        //Task<IEnumerable<User>> GetAll();
        //Task<User> GetByIdAsync(int? id);
        //Task<User> GetByIdAsyncNoTracking(int? id);
        //Task<User> AddToChatroom(string chatroomTitle);
        //Task<User> RemoveFromChatroom(string chatroomTitle);

        //bool Add(Message message);
        //bool Update(Message message);
        //bool Delete(Message message);
        //bool Save();

        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(string id);
        bool Add(User user);
        bool Update(User user);
        bool Delete(User user);
        bool Save();

    }
}
