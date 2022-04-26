using App.Data;
using App.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connectionString = "server=localhost;user=root;password=root_pass;database=app-db";

var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));

builder.Services.AddDbContext<AppDbContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(connectionString, serverVersion)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);

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

app.UseBasicMiddleware();

app.Use(async (context, next) =>
{
    Console.WriteLine("...MW2 ===>");
    await next();
    Console.WriteLine("<=== MW2 ...");
});

// app.MapControllerRoute(
//     name: "listRaces",
//     defaults: new { controller = "Races", action = "List" },
//     pattern: "Races"
//     );

// This pattern is already declared by Home route and is the default "case" if none of the patterns above match
// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller}/{action}/{id?}");

// Cette config dit à ASP.NET : utilise le pattern par défaut controllerName/controllerAction 
// avec un segment optionnel "?" qui sera injecté en tant que paramètre nommé "id" dans l'action
// Si la route ne contient aucune "info" typiquement "/" alors utilise le controller Home et l'action Index
// app.MapControllerRoute(
//     name: "myGreatRoute",
//     defaults: new { controller = "Races", action = "Create" },
//     pattern: "races/createOne"
//     );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.EnsureCreated();
    scope.ServiceProvider.GetRequiredService<AppDbContext>().Seed();
}

app.Run();
