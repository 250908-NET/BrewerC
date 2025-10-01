using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using School.DTO;
using School.Models;
using School.Services;
using Serilog;

namespace School.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // "/students
    public class InstructorsController : ControllerBase
    {
        // Fields
        private readonly ILogger<InstructorsController> _logger;
        private readonly IMapper _mapper;
        private readonly IInstructorService _service;

        // Constructor
        public InstructorsController(ILogger<InstructorsController> logger, IMapper mapper, IInstructorService service)
        {
            _logger = logger;
            _mapper = mapper;
            _service = service;
        }

        // Methods
        
        // Get All
        [HttpGet(Name = "GetAllInstructors")]
        // "/instructors"
        public async Task<IActionResult> GetAllAsync()
        {
            // all private values are already present...
            _logger.LogInformation("Getting all instructors");
            return Ok(_mapper.Map<List<InstructorDTO>>(await _service.GetAllAsync()));
        }

        // Get By Id
        [HttpGet("{id}", Name = "GetInstructorById")]
        // "/instructors/{id}"
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            _logger.LogInformation("Getting instructor {id}", id);
            var instructor = await _service.GetByIdAsync(id);
            return instructor is not null ? Ok(_mapper.Map<InstructorDTO>(instructor)) : NotFound();
        }

        // Create
        [HttpPost(Name = "CreateInstructor")]
        // "/instructors"
        public async Task<IActionResult> CreateAsync(Instructor instructor)
        {
            _logger.LogInformation("Creating instructor");
            await _service.CreateAsync(instructor);
            return Created($"/instructors/{instructor.Id}", _mapper.Map<InstructorDTO>(instructor));
        }

        // Update
        [HttpPut("{id}", Name = "UpdateInstructor")]
        // "/instructors/{id}"
        public async Task<IActionResult> UpdateAsync(string id, Instructor instructor)
        {
            _logger.LogInformation("Updating instructor {id}", id);
            if (! await _service.Exists(id)) 
            {
                return BadRequest();
            }

            await _service.UpdateAsync(id, instructor);
            return Ok(_mapper.Map<InstructorDTO>(await _service.GetByIdAsync(id)));
        }

        // Delete
        [HttpDelete("{id}", Name = "DeleteInstructor")]
        // "/instructors/{id}"
        public async Task<IActionResult> DeleteAsync(string id)
        {
            _logger.LogInformation("Deleting instructor {id}", id);
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}