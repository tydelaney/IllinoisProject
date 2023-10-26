using IllinoisProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Models;

var builder = WebApplication.CreateBuilder(args);

var connection = $"Server=(localdb)\\mssqllocaldb;Database=" +
    $"IllinoisDb;" +
    $"Trusted_Connection=True;MultipleActiveResultSets=true";
builder.Services.AddDbContext<AccountDbContext>(options =>
options.UseSqlServer(connection));



builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AccountDbContext>().AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");



app.Run();