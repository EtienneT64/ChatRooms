using ChatRooms.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatRooms.Data
{
    public class ChatroomContext : IdentityDbContext<User>
    {
        public ChatroomContext(DbContextOptions<ChatroomContext> options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Chatroom> Chatrooms { get; set; }
    }
}
