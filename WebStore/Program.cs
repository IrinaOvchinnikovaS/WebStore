using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Infrastructure.Conventions;
using WebStore.Infrastructure.Middleware;
using WebStore.Services;
using WebStore.Services.Interfaces;
using WebStore.Services.InMemory;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddControllersWithViews(opt =>
{
    opt.Conventions.Add(new TestConvention());
}); //добавление системы MVC

services.AddDbContext<WebStoreDB>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

services.AddTransient<IDbInitializer, DbInitializer>();

services.AddSingleton<IEmployeesData, InMemoryEmployeesData>(); //Singleton - потому что InMemory располагаются
services.AddSingleton<IProductData, InMemoryProductData>();     // Singleton - потому что InMemory располагаются

//отсюда формирование конвейера
var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var db_initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    await db_initializer.InitializeAsync(RemoveBefore: false);
}

//для перехвата исключений и отображения в браузере (в режиме разработки)
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.Map("/testpath", async context => await context.Response.WriteAsync("Test middleware"));

app.UseStaticFiles();

app.UseRouting(); //добаавили систему маршрутизации

app.UseMiddleware<TestMiddleware>();

app.UseWelcomePage("/welcome");

//app.MapDefaultControllerRoute(); //добавлена обработка входящих подключений системы MVC (аналогия ниже)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
