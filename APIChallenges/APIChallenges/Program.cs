var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<WeatherService>();
var app = builder.Build();

// app.MapGet("/", () => "Hello World!");

// git checkout -b api-challenges-{yourName}
// CHALLENGE 1: Nizer


// CHALLENGE 2: Victor


// CHALLENGE 3: Christian

// CHALLENGE 4: Satar


// CHALLENGE 5: Nizer


// CHALLENGE 6: Victor


// CHALLENGE 7: Christian


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



app.Run();
