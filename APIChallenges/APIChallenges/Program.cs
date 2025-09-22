using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<WeatherService>();
builder.Services.AddSingleton<StringService>();
builder.Services.AddSingleton<TemperatureService>();
builder.Services.AddSingleton<CalculatorService>();
builder.Services.AddSingleton<ColorsService>();
builder.Services.AddSingleton<UnitConverterService>();
// builder.Services.AddSingleton<GuessGameService>(sp => new GuessGameService(1, 21));
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

app.MapGet("/", () => {
    return Results.Ok("Hello World");
});

app.MapGet("/calculator/add/{a}/{b}", (double a, double b, CalculatorService service) => {
    return Results.Ok(service.Add(a, b));
});

app.MapGet("/calculator/subtract/{a}/{b}", (double a, double b, CalculatorService service) =>
{
    return Results.Ok(service.Subtract(a, b));
});

app.MapGet("/calculator/multiply/{a}/{b}", (double a, double b, CalculatorService service) =>
{
    return Results.Ok(service.Multiply(a, b));
});

app.MapGet("/calculator/divide/{a}/{b}", (double a, double b, CalculatorService service) =>
{
    return service.Divide(a, b); 
});

// CHALLENGE 2: Victor

// Reverse text
app.MapGet("/text/reverse/{text}", (StringService stringService, string text) =>
{
    return stringService.ReverseText(text);
});

// Uppercase text
app.MapGet("/text/uppercase/{text}", (StringService stringService, string text) =>
{
    return stringService.UppercaseText(text);
});

// Lowercase text
app.MapGet("/text/lowercase/{text}", (StringService stringService, string text) =>
{
    return stringService.LowercaseText(text);
});

// Count characters, words, vowels
app.MapGet("/text/count/{text}", (StringService stringService, string text) =>
{
    return stringService.CountText(text);
});

// Check palindrome
app.MapGet("/text/palindrome/{text}", (StringService stringService, string text) =>
{
    return stringService.CheckPalindrome(text);
});

// CHALLENGE 3: Christian
var numberGroup = app.MapGroup("/number");
numberGroup.MapGet("/fizzbuzz/{count}", (int count) => Challenge3.fizzbuzz(count));
numberGroup.MapGet("/prime/{number}", (int number) => Challenge3.isPrime(number));
numberGroup.MapGet("/fibonacci/{count}", (int count) => Challenge3.fibonacci(count));
numberGroup.MapGet("/factor/{number}", (int number) => Challenge3.factor(number));

// CHALLENGE 4: Satar


// CHALLENGE 5: Nizer
app.MapGet("/colors", (ColorsService service) =>
{
return Results.Ok(service.getColors());
});

app.MapGet("/colors/random", (ColorsService service) =>
{
    return Results.Ok(service.getRandomColor());
});

app.MapGet("/colors/search/{letter}", (ColorsService service, string letter) =>
{
    return Results.Ok(service.searchColor(letter));
});

app.MapPost("/colors/add/{color}", (ColorsService service, string color) =>
{
    service.addColor(color);
    return Results.Ok($"{color} added Successfully");
});
// CHALLENGE 6: Victor
app.MapGet("/temp/celsius-to-fahrenheit/{temp}", (TemperatureService temperatureService, double temp) =>
{
    return temperatureService.CelsiusToFahrenheit(temp);
});

app.MapGet("/temp/fahrenheit-to-celsius/{temp}", (TemperatureService temperatureService, double temp) =>
{
    return temperatureService.FahrenheitToCelsius(temp);
});

app.MapGet("/temp/kelvin-to-celsius/{temp}", (TemperatureService temperatureService, double temp) =>
{
    return temperatureService.KelvinToCelsius(temp);
});

app.MapGet("/temp/celsius-to-kelvin/{temp}", (TemperatureService temperatureService, double temp) =>
{
    return temperatureService.CelsiusToKelvin(temp);
});

app.MapGet("/temp/compare/{temp1}/{unit1}/{temp2}/{unit2}", 
    (double temp1, string unit1, double temp2, string unit2, TemperatureService temperatureService) =>
{
    return temperatureService.CompareTemperatures(temp1, unit1, temp2, unit2);
});

// CHALLENGE 7: Christian
var passwordGroup = app.MapGroup("/password");
passwordGroup.MapGet("/simple/{length}", (int length) => Challenge7.Simple(length));
passwordGroup.MapGet("/complex/{length}", (int length) => Challenge7.Complex(length));
passwordGroup.MapGet("/memorable/{count}", (int count) => Challenge7.Memorable(count));
passwordGroup.MapGet("/strength/{password}", (string password) => Challenge7.Strength(password));

// CHALLENGE 8: Satar


// CHALLENGE 9: Nizar
var convertGroup = app.MapGroup("/convert");
convertGroup.MapGet("/length/{value}/{fromUnit}/{toUnit}", (UnitConverterService unitConverterService, double value, string fromUnit, string toUnit) =>
{
    var result = unitConverterService.convertLength(value, fromUnit, toUnit);
    return Results.Ok(new { value, fromUnit, toUnit, result });
});

convertGroup.MapGet("/weight/{value}/{fromUnit}/{toUnit}", (UnitConverterService unitConverterService, double value, string fromUnit, string toUnit) =>
{
    var result = unitConverterService.convertWeight(value, fromUnit, toUnit);
    return Results.Ok(new { value, fromUnit, toUnit, result });
});

convertGroup.MapGet("/volume/{value}/{fromUnit}/{toUnit}", (UnitConverterService unitConverterService, double value, string fromUnit, string toUnit) =>
{
    var result = unitConverterService.convertVolume(value, fromUnit, toUnit);
    return Results.Ok(new { value, fromUnit, toUnit, result });
});


// CHALLENGE 10:
// TODO: List of forecasts - list of strings
// TODO: DELETE /weather/removeForecast/{index} - remove forecast by index
app.MapGet("/weather", (WeatherService weatherService) =>
{
    return Results.Ok(weatherService.GetAllForecasts());
});

// TODO: POST /weather/saveForecast - save a forecast
app.MapPost("/weather", (WeatherService weatherService, Weather forecast) =>
{
    if (string.IsNullOrWhiteSpace(forecast.Forecast))
        return Results.BadRequest("Forecast text cannot be empty");

    weatherService.SaveForecast(forecast);
    return Results.Created($"/weather/{forecast.Date}", forecast);
});
app.MapDelete("/weather/{date}", (WeatherService weatherService, DateTime date) =>
{
    var deleted = weatherService.DeleteForecast(date);
    return deleted ? Results.NoContent() : Results.NotFound($"No forecast found for {date:yyyy-MM-dd}");
});
// CHALLENGE 11:
var gameGroup = app.MapGroup("/game");
gameGroup.MapGet("/guess-number", (int number, string name) => {
    return Challenge11.GuessNumber(number, name);
});
gameGroup.MapGet("/rock-paper-scissors/{choice}", (string choice) =>
{
    return Challenge11.RockPaperScissors(choice);
});
gameGroup.MapGet("/dice/{sides}/{count}", (int sides, int count) => {
    return Challenge11.RollDice(sides, count);
});
gameGroup.MapGet("/coin-flip/{count}", (int count) => {
    return Challenge11.CoinFlip(count);
});


// BAX GUESS GAME
// To pass parameters into the service, use AddSingleton with a factory lambda:
// var baxGameGroup = app.MapGroup("/bax-guess-game");
// baxGameGroup.MapGet("/getGameInfo", (GuessGameService guessGameService) =>
// {
//     return guessGameService.GetGameInfo();
// });
// baxGameGroup.MapGet("/getGameEvents", (GuessGameService guessGameService) =>
// {
//     return guessGameService.GetEvents();
// });
// baxGameGroup.MapGet("/guessNumber", (GuessGameService guessGameService, int number, string name) =>
// {
//     return guessGameService.GuessNumber(number, name);
// });

app.Run();
