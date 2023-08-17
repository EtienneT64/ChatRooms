using ChatRooms.Data;
using ChatRooms.Interfaces;
using ChatRooms.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatRooms.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ChatroomContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(ChatroomContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Chatroom>> GetAllUserChatrooms()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Message>> GetAllUserMessages()
        {
            var currUser = _httpContextAccessor.HttpContext?.User;
            var userMessages = _context.Messages.Where(c => c.User.Id == currUser.ToString());
            return userMessages.ToList();
        }
        public async Task<User> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetByIdNoTracking(string id)
        {
            return await _context.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Update(User user)
        {
            _context.Users.Update(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
