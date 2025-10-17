using F1API.Data;  // coincide con el namespace de F1Repository

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<F1Repository>();

var app = builder.Build();

app.MapControllers();

app.Run();


