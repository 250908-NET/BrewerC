using System;
using Serilog;

namespace ToDoApp
{
    public class Program
    {
        static void Main(string[] args) 
        {
            // Console.WriteLine("Hello, World!"); // from our console app... bye bye!

            // Serilog - a logging library for .NET applications
            // dotnet add <project .csproj> package <package name>
            // dotnet add ToDoApp.csproj package Serilog.AspNetCore
            // dotnet add package Serilog.Sinks.Console

            // logging sinks - where the logs go
            // console, file, database, remote server, etc.

           var builder = WebApplication.CreateBuilder(args); // Create a builder for the web application
           
           // dotnet add package Microsoft.AspNetCore.OpenApi
           // dotnet add package Swashbuckle.AspNetCore

           // Add Swagger/OpenAPI support
           builder.Services.AddOpenApi();
           builder.Services.AddEndpointsApiExplorer();
           builder.Services.AddSwaggerGen();

           builder.Services.AddScoped<IMathService, MathService>(); // Register MathService with dependency injection

           // configure logging before we "build" the app
           Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
           builder.Host.UseSerilog(); // Use Serilog for logging
           
           var app = builder.Build(); // Build the web application

            if (!app.Environment.IsDevelopment()) // If the environment is not development 
            // (usually we do this the other way around, so that we don't publicize swagger)
            {
                app.MapOpenApi();
                app.UseSwagger(); // Enable Swagger middleware
                app.UseSwaggerUI(); // Enable Swagger UI middleware
            }

            app.MapGet("/", (ILogger<Program> logger) => {
                logger.LogInformation("Hello World endpoint was called"); // Log an information message
                return "Hello World!";
            }); // Map a GET request to the root URL to return "Hello World!"

            // route parameters - part of the URL
            // localhost:5000/getsomething/richard/30
            app.MapGet("/getsomething/{name}/{age}", (ILogger<Program> logger, string name, int age) => {
                logger.LogInformation("Hello World endpoint was called"); // Log an information message
                return $"Hello {name}, you are {age} years old!";
            });

            // query parameters - after the ? in the URL
            // localhost:500/getsomething?name=richard&age=30

            // From a body - usually JSON, XML, text
            // [FromBody] - annotation 
            // from body is unnecessary if the value is a complex type (class, record, etc)
            // native value types will first try to pull from the route, or the query parameters
                // to make it pull from the body, we need to use [FromBody]

            // app.MapPost("/value", (ILogger<Program> logger, [FromBody] <object> newValue) => {
            //     logger.LogInformation("Hello World endpoint was called"); // Log an information message
            //     return "Hello World!";
            // });

            app.MapGet("/square/{number}", (ILogger<Program> logger, int number, IMathService mathService) => {
                logger.LogInformation("Square endpoint was called"); // Log an information message
                var result = mathService.Square(number);
                return Results.Ok(result);
            });

            app.MapPost("/area", (ILogger<Program> logger, AreaRequest request, IMathService mathService) => {
                logger.LogInformation("Area endpoint was called"); // Log an information message
                var result = mathService.CalculateArea(request.Width, request.Height);
                return Results.Ok(result);
            });

            // HTTP requests - responses have body, and headers
            // a HEAD request is like a GET request but without the  - the metadata

            // Put, Post, Patch - include a body
            // Get, Delete, Head, Options - do not include a body

            app.Run(); // Run the web application

            // LOGGING - record the function/activity/behaviors/events of an application
            // events - requests, responses, system/application status, errors, warnings, crashes/shutdowns, startup

            // levels of logging:
            // Trace - most detailed, used for diagnosing issues - everything that happens!
            // Debug - less detailed, used for debugging issues - things that are useful to developers
            // Information - general information about the application's operation - things that are useful to users
            // ----- this is the last "everything is ok" level
            // Warning - something unexpected happened, but the application is still running - things that might require attention
            // Error - something went wrong, the application might be unable to perform a function - things that require immediate attention
            // Critical - something went very wrong, the application might be unable to continue running - things that require immediate attention and action

        }
    }

    public record AreaRequest( double Width, double Height );

    public interface IMathService
    {
        public double Square( double number );
        public double CalculateArea( double width, double height );
    }

    public class MathService : IMathService
    {
        public double Square( double number )
        {
            if (number < 0)
            {
                throw new ArgumentException("Number must be non-negative", nameof(number));
            }
            return number * number;
        }

        public double CalculateArea( double width, double height )
        {
            return width * height;
        }
    }
}