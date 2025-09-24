using Microsoft.EntityFrameworkCore;
using School.Data;
using School.Models;
using School.Services;
using School.Repositories;
using Serilog;
using System.Text.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

string CS = File.ReadAllText("../connection_string.env");

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SchoolDbContext>(options => options.UseSqlServer(CS));

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IInstructorService, InstructorService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.ConfigureHttpJsonOptions(options=>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger(); // read from appsettings.json
builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// -------------------------- ROOT -------------------------- //
app.MapGet("/", () => {
    return "Hello world";
});


// -------------------------- STUDENT -------------------------- //
app.MapGet("/students", async (ILogger<Program> logger, IStudentService service) => 
{
    logger.LogInformation("Getting all students");
    return Results.Ok(await service.GetAllAsync());
});

app.MapGet("/students/{id}", async (ILogger<Program> logger, IStudentService service, int id) =>
{
    logger.LogInformation("Getting student {id}", id);
    var student = await service.GetByIdAsync(id);
    return student is not null ? Results.Ok(student) : Results.NotFound();
});

app.MapPost("/students", async (ILogger<Program> logger, IStudentService service, Student student) =>
{
    logger.LogInformation("Creating student");
    await service.CreateAsync(student);
    return Results.Created($"/students/{student.Id}", student);
});

app.MapPut("/students/{id}", async (ILogger<Program> logger, IStudentService service, int id, Student student) =>
{
    logger.LogInformation("Updating student {id}", id);
    if (! await service.Exists(id)) 
    {
        return Results.BadRequest();
    }

    await service.UpdateAsync(id, student);
    return Results.Ok(await service.GetByIdAsync(id));
});

app.MapDelete("/students/{id}", async (ILogger<Program> logger, IStudentService service, int id) =>
{
    logger.LogInformation("Deleting student {id}", id);
    await service.DeleteAsync(id);
    return Results.NoContent();
});


// -------------------------- INSTRUCTOR -------------------------- //
app.MapGet("/instructors", async (ILogger<Program> logger, IInstructorService service) => 
{
    logger.LogInformation("Getting all instructors");
    return Results.Ok(await service.GetAllAsync());
});

app.MapGet("/instructors/{id}", async (ILogger<Program> logger, IInstructorService service, int id) =>
{
    logger.LogInformation("Getting instructor {id}", id);
    var instructor = await service.GetByIdAsync(id);
    return instructor is not null ? Results.Ok(instructor) : Results.NotFound();
});

app.MapPost("/instructors", async (ILogger<Program> logger, IInstructorService service, Instructor instructor) =>
{
    logger.LogInformation("Creating instructor");
    await service.CreateAsync(instructor);
    return Results.Created($"/instructors/{instructor.Id}", instructor);
});

app.MapPut("/instructors/{id}", async (ILogger<Program> logger, IInstructorService service, int id, Instructor instructor) =>
{
    logger.LogInformation("Updating instructor {id}", id);
    if (! await service.Exists(id)) 
    {
        return Results.BadRequest();
    }

    await service.UpdateAsync(id, instructor);
    return Results.Ok(await service.GetByIdAsync(id));
});

app.MapDelete("/instructors/{id}", async (ILogger<Program> logger, IInstructorService service, int id) =>
{
    logger.LogInformation("Deleting instructor {id}", id);
    await service.DeleteAsync(id);
    return Results.NoContent();
});


// -------------------------- COURSE -------------------------- //
app.MapGet("/courses", async (ILogger<Program> logger, ICourseService service) => 
{
    logger.LogInformation("Getting all courses");
    return Results.Ok(await service.GetAllAsync());
});

app.MapGet("/courses/{id}", async (ILogger<Program> logger, ICourseService service, int id) =>
{
    logger.LogInformation("Getting course {id}", id);
    var course = await service.GetByIdAsync(id);
    return course is not null ? Results.Ok(course) : Results.NotFound();
});

app.MapPost("/courses", async (ILogger<Program> logger, ICourseService service, Course course) =>
{
    logger.LogInformation("Creating course");
    var createdCourse = await service.CreateAsync(course);
    return Results.Created($"/courses/{createdCourse.Id}", createdCourse);
});

app.MapPut("/courses/{id}", async (ILogger<Program> logger, ICourseService service, int id, Course course) =>
{
    logger.LogInformation("Updating course {id}", id);
    if (! await service.Exists(id)) 
    {
        return Results.BadRequest();
    }

    await service.UpdateAsync(id, course);
    return Results.Ok(await service.GetByIdAsync(id));
});

app.MapDelete("/courses/{id}", async (ILogger<Program> logger, ICourseService service, int id) =>
{
    logger.LogInformation("Deleting course {id}", id);
    await service.DeleteAsync(id);
    return Results.NoContent();
});


// -------------------------- ENROLLMENT -------------------------- //
app.MapPost("/enrollments/{studentId}/{courseId}", async (ILogger<Program> logger, IStudentService studentService, int studentId, int courseId) =>
{
    logger.LogInformation("Enrolling student {studentId} in course {courseId}", studentId, courseId);
    await studentService.EnrollAsync(studentId, courseId);
    return Results.Ok();
});


app.Run();


