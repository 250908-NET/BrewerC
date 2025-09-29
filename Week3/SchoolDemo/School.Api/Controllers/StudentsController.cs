using Microsoft.AspNetCore.Mvc;
using School.Models;
using School.Services;
using Serilog;

namespace School.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // "/students
    public class StudentsController : ControllerBase
    {
        // Fields
        private readonly ILogger<StudentsController> _logger;
        private readonly IStudentService _service;

        // Constructor
        public StudentsController( ILogger<StudentsController> logger, IStudentService studentService)
        {
            _logger = logger;
            _service = studentService;
        }

        // Methods

        // Get All
        [HttpGet(Name = "GetAllStudents")]
        // "/students"
        public async Task<IActionResult> GetAllAsync()
        {
            // all private values are already present...
            _logger.LogInformation("Getting all students");
            return Ok(await _service.GetAllAsync());
        }

        // Get By Id
        [HttpGet("{id}", Name = "GetStudentById")]
        // "/students/{id}"
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            _logger.LogInformation("Getting student {id}", id);
            var student = await _service.GetByIdAsync(id);
            return student is not null ? Ok(student) : NotFound();
        }

        // Create
        [HttpPost(Name = "CreateStudent")]
        // "/students"
        public async Task<IActionResult> CreateAsync([FromBody] Student student)
        {
            _logger.LogInformation("Creating student {student}", student);
            await _service.CreateAsync(student);
            return Created($"/students/{student.Id}", student);
        }
    }
}