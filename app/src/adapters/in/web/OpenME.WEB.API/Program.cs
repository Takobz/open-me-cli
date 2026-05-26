using OpenME.Infrastructure.DependencyInjection;
using OpenME.WEB.API.Exceptions;
using OpenME.WEB.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

//builder.Logging.AddConsole();

builder.Services.AddExceptionHandler<APIExceptionHandler>();
builder.Services.AddTraceContext();
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllers()
    .AddCustomClientErrorOptions();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddUserUseCases();

var app = builder.Build();

// Passing empty options because of issue:
// https://github.com/dotnet/aspnetcore/issues/62200
app.UseExceptionHandler(_ => {});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
