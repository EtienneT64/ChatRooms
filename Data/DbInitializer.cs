using ChatRooms.Models;
using Microsoft.AspNetCore.Identity;

namespace ChatRooms.Data
{
    public class DbInitializer
    {
        public static void CreateDatabase(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ChatroomContext>();

                context?.Database.EnsureDeleted();
                context?.Database.EnsureCreated();
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                string adminUserName = "etienneT";

                var adminUser = await userManager.FindByNameAsync(adminUserName);
                if (adminUser == null)
                {
                    var newAdminUser = new User()
                    {
                        Id = "bc100ece-cdd0-481a-b0a0-a8ec05dca602",
                        UserName = "etienneT",
                        UserNameColor = "#FFD700",
                        //ProfileImageUrl = xyz
                    };
                    await userManager.CreateAsync(newAdminUser, "Password@64");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserName = "liamV";

                var appUser = await userManager.FindByNameAsync(appUserName);
                if (appUser == null)
                {
                    var newAppUser = new User()
                    {
                        Id = "bbd74782-525a-4c59-9700-5a0b728bf0c6",
                        UserName = "liamV",
                        UserNameColor = "#0000FF",
                        //ProfileImageUrl = xyz
                    };
                    await userManager.CreateAsync(newAppUser, "Password@64");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }

                string appUserName2 = "test";

                var appUser2 = await userManager.FindByNameAsync(appUserName2);
                if (appUser2 == null)
                {
                    var newAppUser2 = new User()
                    {
                        Id = "a6e31363-fc72-4f7e-9238-7f6cada1e68c",
                        UserName = "test",
                        UserNameColor = "#0000FF",
                        //ProfileImageUrl = xyz
                    };
                    await userManager.CreateAsync(newAppUser2, "Password@64");
                    await userManager.AddToRoleAsync(newAppUser2, UserRoles.User);
                }
            }
        }

        public static void Initialize(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ChatroomContext>();

                context?.Database.EnsureCreated();

                if (!context.Chatrooms.Any())
                {
                    context.Chatrooms.AddRange(new List<Chatroom>()
                    {
                        new Chatroom()
                        {
                            Name = "Global Chat X",
                            ChatroomImageUrl = "http://res.cloudinary.com/dzjsiibch/image/upload/v1692533555/glbz9vizpsqt9hwateb6.webp",
                            Description = "This is the town square, all are welcome, free speech",
                            MsgLengthLimit = 320,
                            OwnerId = "bc100ece-cdd0-481a-b0a0-a8ec05dca602"

                         },
                        new Chatroom()
                        {
                            Name = "Fortnite",
                            ChatroomImageUrl = "http://res.cloudinary.com/dzjsiibch/image/upload/v1692532935/j1on5owlohilcs53phad.jpg",
                            Description = "We love fortnite Poggers",
                            MsgLengthLimit = 280,
                            OwnerId = "bc100ece-cdd0-481a-b0a0-a8ec05dca602"

                         },
                       new Chatroom()
                        {
                            Name = "Baldurs Gate 3",
                            ChatroomImageUrl = "http://res.cloudinary.com/dzjsiibch/image/upload/v1692532380/ka3n1304vzib1u8ctsvd.webp",
                            Description = "Dungeons and dragons game with roll playing and dice rolling",
                            MsgLengthLimit = 269,
                            OwnerId = "bc100ece-cdd0-481a-b0a0-a8ec05dca602"
                         },
                        new Chatroom()
                        {
                            Name = "Baldurs Gate 4",
                            ChatroomImageUrl = "http://res.cloudinary.com/dzjsiibch/image/upload/v1692532380/ka3n1304vzib1u8ctsvd.webp",
                            Description = "Dungeons and dragons game with roll playing and dice rolling",
                            MsgLengthLimit = 269,
                            OwnerId = "bc100ece-cdd0-481a-b0a0-a8ec05dca602"
                         },
                         new Chatroom()
                        {
                            Name = "Baldurs Gate 5",
                            ChatroomImageUrl = "http://res.cloudinary.com/dzjsiibch/image/upload/v1692532380/ka3n1304vzib1u8ctsvd.webp",
                            Description = "Dungeons and dragons game with roll playing and dice rolling",
                            MsgLengthLimit = 269,
                            OwnerId = "bc100ece-cdd0-481a-b0a0-a8ec05dca602"
                         },
                           new Chatroom()
                        {
                            Name = "Baldurs Gate 6",
                            ChatroomImageUrl = "http://res.cloudinary.com/dzjsiibch/image/upload/v1692532380/ka3n1304vzib1u8ctsvd.webp",
                            Description = "Dungeons and dragons game with roll playing and dice rolling",
                            MsgLengthLimit = 269,
                            OwnerId = "bc100ece-cdd0-481a-b0a0-a8ec05dca602"
                         },
                           new Chatroom()
                        {
                            Name = "Baldurs Gate 7",
                            ChatroomImageUrl = "http://res.cloudinary.com/dzjsiibch/image/upload/v1692532380/ka3n1304vzib1u8ctsvd.webp",
                            Description = "Dungeons and dragons game with roll playing and dice rolling",
                            MsgLengthLimit = 269,
                            OwnerId = "bc100ece-cdd0-481a-b0a0-a8ec05dca602"
                         },

                    });
                    context.SaveChanges();
                }
                //Messages
                if (!context.Messages.Any())
                {
                    context.Messages.AddRange(new List<Message>()
                    {
                        new Message()
                        {
                            Content = "Hello everyone, how is it going!",
                            Length = "Hello everyone, how is it going!".Length,
                            TimeStamp = DateTime.Parse("2023/08/01"),
                            UserId = "bbd74782-525a-4c59-9700-5a0b728bf0c6",
                            ChatroomId=1,
                        },
                         new Message()
                        {
                            Content = "It not growing so great, my website is a disaster!",
                            Length = "It not growing so great, my website is a disaster!".Length,
                            TimeStamp = DateTime.Parse("2023/08/02"),
                            UserId = "bc100ece-cdd0-481a-b0a0-a8ec05dca602",
                            ChatroomId=1,
                        },
                           new Message()
                        {
                            Content = "Sucks to suck hey",
                            Length = "Sucks to suck hey".Length,
                            TimeStamp = DateTime.Parse("2023/08/03"),
                            UserId = "bbd74782-525a-4c59-9700-5a0b728bf0c6",
                            ChatroomId=1,
                        },
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
