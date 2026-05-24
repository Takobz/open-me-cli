using Microsoft.AspNetCore.Mvc.Infrastructure;
using OpenME.Infrastructure.DependencyInjection;
using OpenME.WEB.API.Errors;
using OpenME.WEB.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IClientErrorFactory, APIClientErrorFactory>();
builder.Services.AddControllers()
    .AddCustomClientErrorOptions();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddUserUseCases();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
