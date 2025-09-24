
using ScaffoldAPI.Models;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ChinookContext>(options =>
{
    var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
    options.UseSqlServer(connectionString);
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

var rootGroup = app.MapGroup("/");
rootGroup.MapGet("", () => "Hello World");

app.Run();
