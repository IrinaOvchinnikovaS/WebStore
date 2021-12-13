var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var config = app.Configuration;

app.MapGet("/", () => config["CustomGreetings"]);

app.Run();
