using DataAccess.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Context>();
builder.Services.AddIdentity<AppUser, AppRole>(x =>
{
    x.Password.RequireUppercase = false;
    x.Password.RequireNonAlphanumeric = false;
})
    .AddEntityFrameworkStores<Context>();
builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, options =>
{
    options.LoginPath = "/Login/Index";
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminRole", policy =>
    {
        policy.RequireRole("Admin");
    });

    options.AddPolicy("AdminOrWorkerRole", policy =>
    {
        policy.RequireRole("Admin", "Worker");
    });

    options.AddPolicy("NoUsertRole", policy =>
    {
        policy.RequireAssertion(context =>
            !context.User.IsInRole("User"));
    });
});

var app = builder.Build();

app.MapControllers();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Ensure static files are served
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
