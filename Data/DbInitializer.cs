using ChatRooms.Models;

namespace ChatRooms.Data
{
    public class DbInitializer
    {
        public static void Initialize(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ChatroomContext>();

                context?.Database.EnsureDeleted();
                context?.Database.EnsureCreated();

                // Look for any users
                if (context.Users.Any())
                {
                    return;   // DB has been seeded
                }

                var chatrooms = new Chatroom[]
             {
            new Chatroom{Name="Global Chat",Description="This is a chat for everyone! Be inclusive and respectful!",UserLimit=5, MsgLengthLimit=320},
             };

                foreach (Chatroom c in chatrooms)
                {
                    context.Chatrooms.Add(c);
                }
                context.SaveChanges();

                var users = new User[]
                {
            new User{Username="Bob123",Password="bob123", DisplayName="Bob",DisplayNameColor="#ffffff", RegisterDate=DateTime.Parse("2023/08/04"),
                //ChatroomId=1
            },
            new User{Username="Larry123",Password="larry123", DisplayName="Larry",DisplayNameColor="#000000", RegisterDate=DateTime.Parse("2023/08/05"), 
                //ChatroomId = 1
            },
            new User{Username="Chad123",Password="chad123", DisplayName="Chad",DisplayNameColor="#fffaaa", RegisterDate=DateTime.Parse("2023/08/06"), 
                //ChatroomId = 1
            },
                };
                foreach (User u in users)
                {
                    context.Users.Add(u);
                }
                context.SaveChanges();

                var messages = new Message[]
                {
            new Message{UserId=1,ChatroomId=1,Content="Hello my name is Bob!", MsgLength=21, SendDate=DateTime.Parse("2023/08/04")},
            new Message{UserId=2,ChatroomId=1,Content="Hello my name is Larry!", MsgLength=23, SendDate=DateTime.Parse("2023/08/05")},
            new Message{UserId=3,ChatroomId=1,Content="Hello my name is Chad!", MsgLength=22, SendDate=DateTime.Parse("2023/08/06")},
                };
                foreach (Message m in messages)
                {
                    context.Messages.Add(m);
                }
                context.SaveChanges();


            }
        }
    }
}
