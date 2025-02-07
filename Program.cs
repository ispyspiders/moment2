using Microsoft.AspNetCore.Session;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); // Aktivera MVC

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // timeout för sessionen
    options.Cookie.HttpOnly = true; // Sätt cookie till httpOnly
    options.Cookie.IsEssential = true; // Gör sessions-cookie nödvändig
});

var app = builder.Build();

app.UseStaticFiles(); // Använd statiska filer
app.UseRouting(); // Använd routing
app.UseSession(); // Använd session

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
