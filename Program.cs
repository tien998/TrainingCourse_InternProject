using UserServices;
using DI;

var builder = WebApplication.CreateBuilder(args);

builder.AddDI();
var app = builder.Build();

app.MapGet("/", () => "Hello World");
app.AddStudentManagementAPI();

app.Run();
