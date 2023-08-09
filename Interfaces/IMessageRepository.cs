using ChatRooms.Models;

namespace ChatRooms.Interfaces
{
    public interface IMessageRepository
    {
        public interface IUserRepository
        {
            Task<IEnumerable<Message>> GetAll();
            Task<User> GetByIdAsync(int id);
            Task<User> GetByIdAsyncNoTracking(int id);

            bool Add(Message message);
            bool Update(Message message);
            bool Delete(Message message);
            bool Save();
        }
    }
}
