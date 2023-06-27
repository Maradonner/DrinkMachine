using DrinkMachine.BL.Cookie;
using DrinkMachine.BL.Services;
using DrinkMachine.BL.Services.Interfaces;
using DrinkMachine.DAL.Data;
using DrinkMachine.DAL.Repositories;
using DrinkMachine.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



var conn = builder.Configuration.GetConnectionString("DefaultConnection")!;

var dir = Environment.CurrentDirectory;

if (conn.Contains("%CONTENTROOTPATH%")) conn = conn.Replace("%CONTENTROOTPATH%", dir);


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options => 
{ 
    options.UseSqlServer(conn);
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddScoped<IDrinkRepository, DrinkRepository>();
builder.Services.AddScoped<IDrinkService, DrinkService>();

builder.Services.AddScoped<IDbSessionRepository, DbSessionRepository>();
builder.Services.AddScoped<IDbSessionService, DbSessionService>();

builder.Services.AddScoped<ICoinService, CoinService>();
builder.Services.AddScoped<ICoinRepository, CoinRepository>();

builder.Services.AddScoped<IWebCookie, WebCookie>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();


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
    "default",
    "{controller=User}/{action=Index}/{id?}");

app.Run();