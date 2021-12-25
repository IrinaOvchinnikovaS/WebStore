using WebStore.Infrastructure.Conventions;
using WebStore.Infrastructure.Middleware;
using WebStore.Services;
using WebStore.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddControllersWithViews(opt =>
{
    opt.Conventions.Add(new TestConvention());
}); //добавление системы MVC

services.AddSingleton<IEmployeesData, InMemoryEmployeesData>(); //Singleton - потому что InMemory располагаются
services.AddSingleton<IProductData, InMemoryProductData>();     // Singleton - потому что InMemory располагаются

//отсюда формирование конвейера
var app = builder.Build();

//для перехвата исключений и отображения в браузере (в режиме разработки)
if(app.Environment.IsDevelopment())
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
