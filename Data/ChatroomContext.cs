using ChatRooms.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatRooms.Data
{
    public class ChatroomContext : DbContext
    {
        public ChatroomContext(DbContextOptions<ChatroomContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chatroom> Chatrooms { get; set; }
    }
}
