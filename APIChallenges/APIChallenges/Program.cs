using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<WeatherService>();
builder.Services.AddSingleton<StringService>();
builder.Services.AddSingleton<TemperatureService>();
builder.Services.AddSingleton<CalculatorService>();
builder.Services.AddSingleton<ColorsService>();
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
app.MapGet("/text/reverse/{text}", (string text, StringService stringService) =>
{
    return stringService.ReverseText(text);
});

// Uppercase text
app.MapGet("/text/uppercase/{text}", (string text, StringService stringService) =>
{
    return stringService.UppercaseText(text);
});

// Lowercase text
app.MapGet("/text/lowercase/{text}", (string text, StringService stringService) =>
{
    return stringService.LowercaseText(text);
});

// Count characters, words, vowels
app.MapGet("/text/count/{text}", (string text, StringService stringService) =>
{
    return stringService.CountText(text);
});

// Check palindrome
app.MapGet("/text/palindrome/{text}", (string text, StringService stringService) =>
{
    return stringService.CheckPalindrome(text);
});

// CHALLENGE 3: Christian

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

app.MapGet("/colors/search/{letter}", (string letter, ColorsService service) =>
{
    return Results.Ok(service.searchColor(letter));
});

app.MapPost("/colors/add/{color}", (string color, ColorsService service) =>
{
    service.addColor(color);
    return Results.Ok($"{color} added Successfully");
});
// CHALLENGE 6: Victor
app.MapGet("/temp/celsius-to-fahrenheit/{temp}", (double temp, TemperatureService temperatureService) =>
{
    return temperatureService.CelsiusToFahrenheit(temp);
});

app.MapGet("/temp/fahrenheit-to-celsius/{temp}", (double temp, TemperatureService temperatureService) =>
{
    return temperatureService.FahrenheitToCelsius(temp);
});

app.MapGet("/temp/kelvin-to-celsius/{temp}", (double temp, TemperatureService temperatureService) =>
{
    return temperatureService.KelvinToCelsius(temp);
});

app.MapGet("/temp/celsius-to-kelvin/{temp}", (double temp, TemperatureService temperatureService) =>
{
    return temperatureService.CelsiusToKelvin(temp);
});

app.MapGet("/temp/compare/{temp1}/{unit1}/{temp2}/{unit2}", 
    (double temp1, string unit1, double temp2, string unit2, TemperatureService temperatureService) =>
{
    return temperatureService.CompareTemperatures(temp1, unit1, temp2, unit2);
});

// CHALLENGE 7: Christian


// CHALLENGE 8: Satar


// CHALLENGE 9:


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



app.Run();
