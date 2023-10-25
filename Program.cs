using IllinoisProject.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connection = $"Server=(localdb)\\mssqllocaldb;Database=" +
    $"IllinoisDb;" +
    $"Trusted_Connection=True;MultipleActiveResultSets=true";
builder.Services.AddDbContext<AccountDbContext>(options =>
options.UseSqlServer(connection));


builder.Services.AddControllersWithViews();
var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "Default",
    pattern: "{controller=BlogPost}/{action=AllBlogPost}/{id?}");

app.Run();