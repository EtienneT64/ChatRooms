using ChatRooms.Data;
using ChatRooms.Interfaces;
using ChatRooms.Models;

namespace ChatRooms.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatroomContext _context;
        public MessageRepository(ChatroomContext context)
        {
            _context = context;
        }
        public bool Add(Message message)
        {
            _context.Add(message);
            return Save();
        }

        public bool Delete(Message message)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Message>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsyncNoTracking(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
