using IllinoisProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connection = $"Server=(localdb)\\mssqllocaldb;Database=" +
    $"IllinoisDb;" +
    $"Trusted_Connection=True;MultipleActiveResultSets=true";
builder.Services.AddDbContext<AccountDbContext>(options =>
options.UseSqlServer(connection));



builder.Services.AddIdentity<Account, IdentityRole>().AddEntityFrameworkStores<AccountDbContext>().AddDefaultTokenProviders();

// Configure Authentication Cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.SlidingExpiration = false; // Disable sliding expiration
    options.Cookie.IsEssential = true; // Make the cookie essential
    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict; // Set SameSite policy
    options.Cookie.Expiration = null; // Remove the expiration time
    options.Cookie.MaxAge = null; // Ensure the cookie is a session cookie
});




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