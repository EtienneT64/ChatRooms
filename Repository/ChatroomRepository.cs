using ChatRooms.Interfaces;
using ChatRooms.Models;

namespace ChatRooms.Repository
{
    public class ChatroomRepository : IChatroomRepository
    {
        public bool Add(Chatroom chatroom)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Chatroom chatroom)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Chatroom>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Chatroom> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Chatroom> GetByIdAsyncNoTracking(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool Update(Chatroom chatroom)
        {
            throw new NotImplementedException();
        }
    }
}
