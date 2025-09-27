
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Serilog;

using ArmaReforger.Data;
using ArmaReforger.DTOs;
using ArmaReforger.Services;
using ArmaReforger.Repository;
using ArmaReforger.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ArmaReforgerDbContext>((options) =>
{
    var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<IPlayerRecordRepository, PlayerRecordRepository>();
builder.Services.AddScoped<IPlayerNameRepository, PlayerNameRepository>();
builder.Services.AddScoped<IPlayerIpAddressRepository, PlayerIpAddressRepository>();
builder.Services.AddScoped<IServerRecordRepository, ServerRecordRepository>();
builder.Services.AddScoped<IServerPlayerConnectionRepository, ServerPlayerConnectionRepository>();

builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IServerService, ServerService>();

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
}

//app.UseHttpsRedirection();

app.Map("/error", (HttpContext httpContext) =>
{
    var exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
    return Results.Problem(
        detail: exception?.Message,
        statusCode: 500
    );
});

app.MapGet("/health", () => "Hello World");

var serverGroup = app.MapGroup("/server");
serverGroup.MapPost("/", ([FromBody] ServerRecordDto serverRecordDto, IServerService serverService) =>
{
    return serverService.RegisterServer(serverRecordDto);
});
serverGroup.MapPut("/", ([FromBody] UpdateServerDto updateServerDto, IServerService serverService) =>
{
    return serverService.UpdateServer(updateServerDto.oldServer, updateServerDto.newServer);
});
serverGroup.MapDelete("/", ([FromBody] ServerRecordDto serverRecordDto, IServerService serverService) =>
{
    return serverService.DeleteServer(serverRecordDto);
});

var playerGroup = app.MapGroup("/player");
playerGroup.MapPost("/serverConnection", ([FromBody] PlayerConnectionDto playerConnection, IPlayerService playerService) =>
{
    PlayerRecordDto playerRecordDto = new PlayerRecordDto(playerConnection.biIdentity);
    PlayerNameDto playerNameDto = new PlayerNameDto(playerRecordDto, playerConnection.playerName);
    PlayerIpAddressDto playerIpAddressDto = new PlayerIpAddressDto(playerRecordDto, playerConnection.playerIpAddress);
    ServerRecordDto serverRecordDto = new ServerRecordDto(playerConnection.serverIpAddress, playerConnection.serverPort, "");
    ServerPlayerConnectionDto serverPlayerConnectionDto = new ServerPlayerConnectionDto(serverRecordDto, playerRecordDto, playerConnection.connectionTime, playerConnection.action);
    return playerService.RegisterPlayerConnection(
        playerRecordDto,
        playerNameDto,
        playerIpAddressDto,
        serverPlayerConnectionDto,
        serverRecordDto
        );
});
playerGroup.MapGet("/name", ([FromBody] NameDto nameDto, IPlayerService playerService) => {
    return playerService.SearchPlayerNames(nameDto.name);
});

app.Run();
