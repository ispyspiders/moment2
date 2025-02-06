var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); // Aktivera MVC

var app = builder.Build();

app.UseStaticFiles(); // Använd statiska filer
app.UseRouting(); // Använd routing

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
