using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<WeatherService>();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
builder.Host.UseSerilog();
builder.Services.AddSingleton<CalculatorService>();
builder.Services.AddSingleton<ColorsService>();
builder.Services.AddSingleton<UnitConverterService>();
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


// CHALLENGE 7: Christian


// CHALLENGE 8: Satar


// CHALLENGE 9: Nizar
app.MapGroup("/convert");
app.MapGet("/convert/length/{value}/{fromUnit}/{toUnit}", (double value, string fromUnit, string toUnit, UnitConverterService service) =>
{
    var result = service.convertLength(value, fromUnit, toUnit);
    return Results.Ok(new { value, fromUnit, toUnit, result });
});

app.MapGet("/convert/weight/{value}/{fromUnit}/{toUnit}", (double value, string fromUnit, string toUnit, UnitConverterService service) =>
{
    var result = service.convertWeight(value, fromUnit, toUnit);
    return Results.Ok(new { value, fromUnit, toUnit, result });
});

app.MapGet("/convert/volume/{value}/{fromUnit}/{toUnit}", (double value, string fromUnit, string toUnit, UnitConverterService service) =>
{
    var result = service.convertVolume(value, fromUnit, toUnit);
    return Results.Ok(new { value, fromUnit, toUnit, result });
});


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



app.Run();
