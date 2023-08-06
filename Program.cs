using ChatRooms.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "initialize")
{
    try
    {
        DbInitializer.Initialize(app);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message); // placeholder
    }
}

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDbContext<ChatroomContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
