var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddControllersWithViews(); //���������� ������� MVC

var app = builder.Build();

//��� ��������� ���������� � ����������� � �������� (� ������ ����������)
if(app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting(); //��������� ������� �������������

var config = app.Configuration;

//app.MapGet("/", () => config["CustomGreetings"]);
app.MapGet("/throw", () =>
{
    throw new ApplicationException("������ � ���������!");
});

//app.MapDefaultControllerRoute(); //��������� ��������� �������� ����������� ������� MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
