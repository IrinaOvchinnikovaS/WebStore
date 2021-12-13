var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddControllersWithViews();

var app = builder.Build();

//для перехвата исключений в браузере
if(app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

var config = app.Configuration;

//app.MapGet("/", () => config["CustomGreetings"]);
app.MapGet("/throw", () =>
{
    throw new ApplicationException("Ошибка в программе!");
});

app.MapDefaultControllerRoute();

app.Run();
