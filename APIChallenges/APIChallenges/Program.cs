using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<WeatherService>();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
builder.Host.UseSerilog();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.MapGet("/", () => "Hello World!");

// git checkout -b api-challenges-{yourName}
// CHALLENGE 1: Nizer


// CHALLENGE 2: Victor


// CHALLENGE 3: Christian
var numberGroup = app.MapGroup("/number");
numberGroup.MapGet("/fizzbuzz/{count}", (int count) => Challenge3.fizzbuzz(count));
numberGroup.MapGet("/prime/{number}", (int number) => Challenge3.isPrime(number));
numberGroup.MapGet("/fibonacci/{count}", (int count) => Challenge3.fibonacci(count));
numberGroup.MapGet("/factor/{number}", (int number) => Challenge3.factor(number));

// CHALLENGE 4: Satar


// CHALLENGE 5: Nizer


// CHALLENGE 6: Victor


// CHALLENGE 7: Christian
var passwordGroup = app.MapGroup("/password");
passwordGroup.MapGet("/simple/{length}", (int length) => Challenge7.Simple(length));
passwordGroup.MapGet("/complex/{length}", (int length) => Challenge7.Complex(length));
passwordGroup.MapGet("/memorable/{count}", (int count) => Challenge7.Memorable(count));
passwordGroup.MapGet("/strength/{password}", (string password) => Challenge7.Strength(password));

// CHALLENGE 8: Satar


// CHALLENGE 9:


// CHALLENGE 10:
// TODO: List of forecasts - list of strings
// TODO: POST /weather/saveForecast - save a forecast
// TODO: GET /weather/ - whole list
// TODO: DELETE /weather/removeForecast/{index} - remove forecast by index
var weatherGroup = app.MapGroup("/weather");
weatherGroup.MapPost("/saveForecast", (WeatherService weatherService, Weather weather) =>
{
    return Results.Ok(weatherService.SaveForecast(weather.forecast));
});
weatherGroup.MapGet("/", (WeatherService service) =>
{
    return Results.Ok(service.getAllForecasts());
});
weatherGroup.MapDelete("/removeForecast/{index}", (WeatherService weatherService, int index) =>
{
    return Results.Ok(weatherService.deleteForecast(index));
});

// CHALLENGE 11:
var gameGroup = app.MapGroup("/game");
gameGroup.MapGet("/guess-number/{number}", (int number) => {
    Challenge11.GuessNumber(number);
});
gameGroup.MapGet("/rock-paper-scissors/{choice}", (string choice) =>
{
    return Challenge11.RockPaperScissors(choice);
});
gameGroup.MapGet("/dice?sides={sides}&count={count}", (int sides, int count) => {
    return Challenge11.RollDice(sides, count);
});
gameGroup.MapGet("/coin-flip/{count}", (int count) => {
    return Challenge11.CoinFlip(count);
});



app.Run();
