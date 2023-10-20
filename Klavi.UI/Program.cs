using Castle.Core.Smtp;
using Kalvi.Core.Entities;
using Kalvi.DataAccess.Contexts;
using Klavi.UI.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
//using IEmailSender = Klavi.UI.Helper.IEmailSender;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>(identityOptions => {
    identityOptions.User.RequireUniqueEmail = true;

    identityOptions.Password.RequireNonAlphanumeric = true;
    identityOptions.Password.RequiredLength = 8;
    identityOptions.Password.RequireDigit = true;
    identityOptions.Password.RequireLowercase = true;
    identityOptions.Password.RequireUppercase = true;

    identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
    identityOptions.Lockout.MaxFailedAccessAttempts = 3;
    identityOptions.Lockout.AllowedForNewUsers = true;
}).AddEntityFrameworkStores<AppDbContext>()
  .AddDefaultTokenProviders();

// Inside ConfigureServices method in Startup.cs
//builder.Services.AddTransient<IEmailSender, EmailSender>(); // Replace SmtpEmailSender with your implementation
//builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.ConfigureApplicationCookie(option =>
{
    option.LoginPath = "/Auth/Login";
});

var app = builder.Build();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllerRoute(
    name:"areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);

app.MapControllerRoute(
        name:"Default",
        pattern:"{controller=Home}/{action=Index}/{id?}"
);

app.Run();
