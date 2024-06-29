using Microsoft.EntityFrameworkCore;
using Vendre_pieces_auto.Data;
using Auth0.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Vendre_pieces_auto.Models.Tabels;
using Microsoft.AspNetCore.Identity.UI.Services;
using Vendre_pieces_auto.Service;
using Microsoft.Extensions.Options;


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
        options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
        options.ResponseType = "code";
        options.Scope = "openid profile email picture";
       
    });
builder.Services.AddScoped<IAccessTocken, AccessToken>();


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
