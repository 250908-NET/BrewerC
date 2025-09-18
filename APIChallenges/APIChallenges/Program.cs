var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<CalculatorService>();
builder.Services.AddSingleton<ColorsService>();
var app = builder.Build();

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


// CHALLENGE 9:


// CHALLENGE 10:


// CHALLENGE 11:



app.Run();
