var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddControllersWithViews(); //добавление системы MVC

var app = builder.Build();

//для перехвата исключений и отображения в браузере (в режиме разработки)
if(app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.UseRouting(); //добаавили систему маршрутизации

var config = app.Configuration;

//app.MapGet("/", () => config["CustomGreetings"]);
app.MapGet("/throw", () =>
{
    throw new ApplicationException("Ошибка в программе!");
});

//app.MapDefaultControllerRoute(); //добавлена обработка входящих подключений системы MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
