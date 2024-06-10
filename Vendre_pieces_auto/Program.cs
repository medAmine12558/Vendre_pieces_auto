using Microsoft.EntityFrameworkCore;
using Vendre_pieces_auto.Data;
using Auth0.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Context>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("connString")));

builder.Services
    .AddAuth0WebAppAuthentication(options => {
        options.Domain = builder.Configuration["Auth0:Domain"];
        options.ClientId = builder.Configuration["Auth0:ClientId"];
    }); 

builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User_Interface}/{action=InterfaceUser}/{id?}");

app.Run();
