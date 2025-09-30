using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using School.DTO;
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
        private readonly IMapper _mapper;
        private readonly IStudentService _service;

        // Constructor
        public StudentsController( ILogger<StudentsController> logger, IMapper mapper, IStudentService studentService)
        {
            _logger = logger;
            _mapper = mapper;
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

            var students = await _service.GetAllAsync();

            // long-hand DTO mapping
            var studentDTOs = students.Select(s => new StudentDTO
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                UserType = s.UserType,
                Courses = s.Courses.Select(c => new CourseDTO
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description
                }).ToList()
            });
            

            return Ok(studentDTOs);
        }

        // Get By Id
        [HttpGet("{id}", Name = "GetStudentById")]
        // "/students/{id}"
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            _logger.LogInformation("Getting student {id}", id);
            var student = await _service.GetByIdAsync(id);
            return student is not null ? Ok(_mapper.Map<StudentDTO>(student)) : NotFound();
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

        // Update
        [HttpPut("{id}", Name = "UpdateStudent")]
        // "/students/{id}"
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Student student)
        {
            _logger.LogInformation("Updating student {id}", id);
            if (! await _service.Exists(id)) 
            {
                return BadRequest();
            }
            await _service.UpdateAsync(id, student);
            return Ok(await _service.GetByIdAsync(id));
        }

        // Delete
        [HttpDelete("{id}", Name = "DeleteStudent")]
        // "/students/{id}"
        public async Task<IActionResult> DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting student {id}", id);
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}