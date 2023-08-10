using ChatRooms.Data;
using ChatRooms.Interfaces;
using ChatRooms.Models;

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
        public async Task<List<Message>> GetAllUserChatrooms()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Message>> GetAllUserMessages()
        {
            var currUser = _httpContextAccessor.HttpContext?.User;
            var userMessages = _context.Messages.Where(c => c.User.Id == currUser.ToString());
            return userMessages.ToList();

        }
    }
}
