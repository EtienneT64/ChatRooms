using ChatRooms.Data;
using ChatRooms.Interfaces;
using ChatRooms.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatRooms.Repository
{
    public class ChatroomRepository : IChatroomRepository
    {
        private readonly ChatroomContext _context;

        public ChatroomRepository(ChatroomContext context)
        {
            _context = context;
        }
        public bool Add(Chatroom chatroom)
        {
            _context.Add(chatroom);
            return Save();
        }

        public bool Delete(Chatroom chatroom)
        {
            _context.Remove(chatroom);
            return Save();
        }

        public async Task<IEnumerable<Chatroom>> GetAll()
        {
            return await _context.Chatrooms.ToListAsync();
        }

        public async Task<Chatroom> GetByIdAsync(int? id)
        {
            return await _context.Chatrooms.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Chatroom> GetByIdAsyncNoTracking(int? id)
        {
            return await _context.Chatrooms.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public string? GetNameById(int id)
        {
            var chatroom = _context.Chatrooms
                .Where(c => c.Id == id)
                .Select(c => c.Name)
                .FirstOrDefault();

            return chatroom;
        }

        public async Task<IEnumerable<Message>> GetMessagesByChatroomId(int? id)
        {
            return await _context.Messages
                .Where(message => message.ChatroomId == id)
                .Take(10).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Chatroom chatroom)
        {
            _context.Update(chatroom);
            return Save();
        }
    }
}
