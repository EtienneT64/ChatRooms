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
                string adminUserEmail = "etiennetheunissen64@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new User()
                    {
                        Id = "bc100ece-cdd0-481a-b0a0-a8ec05dca602",
                        UserName = "etienneT",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        DisplayNameColor = "#FFD700",
                        //ProfileImageUrl = xyz
                    };
                    await userManager.CreateAsync(newAdminUser, "Password@64");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "liamventer460@gmail.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new User()
                    {
                        Id = "bbd74782-525a-4c59-9700-5a0b728bf0c6",
                        UserName = "liamV",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        DisplayNameColor = "#0000FF",
                        //ProfileImageUrl = xyz
                    };
                    await userManager.CreateAsync(newAppUser, "Password@64");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }

                string appUserEmail2 = "test@gmail.com";

                var appUser2 = await userManager.FindByEmailAsync(appUserEmail2);
                if (appUser2 == null)
                {
                    var newAppUser2 = new User()
                    {
                        Id = "a6e31363-fc72-4f7e-9238-7f6cada1e68c",
                        UserName = "liamV",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        DisplayNameColor = "#0000FF",
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
                            //ChatroomImageUrl = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the town square, all are welcome, free speech",
                            UserLimit = 50,
                            MsgLengthLimit = 320,

                         },
                        new Chatroom()
                        {
                            Name = "Fortnite",
                            //ChatroomImageUrl = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "We love fortnite Poggers",
                            UserLimit = 25,
                            MsgLengthLimit = 280,

                         },
                       new Chatroom()
                        {
                            Name = "Baldurs Gate 3",
                            //ChatroomImageUrl = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "Dungeons and dragons game with roll playing and dice rolling",
                            UserLimit = 49,
                            MsgLengthLimit = 269,

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
                            MsgLength = "Hello everyone, how is it going!".Length,
                            SendDate = DateTime.Parse("2023/08/01"),
                            UserId = "bbd74782-525a-4c59-9700-5a0b728bf0c6",
                            ChatroomId=1,
                        },
                         new Message()
                        {
                            Content = "It not growing so great, my website is a disaster!",
                            MsgLength = "It not growing so great, my website is a disaster!".Length,
                            SendDate = DateTime.Parse("2023/08/02"),
                            UserId = "bc100ece-cdd0-481a-b0a0-a8ec05dca602",
                            ChatroomId=1,
                        },
                           new Message()
                        {
                            Content = "Sucks to suck hey",
                            MsgLength = "Sucks to suck hey".Length,
                            SendDate = DateTime.Parse("2023/08/03"),
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
