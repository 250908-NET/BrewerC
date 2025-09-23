using School.Models;
using School.Repositories;

namespace School.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repo;

        public CourseService(ICourseRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Course>> GetAllAsync() => _repo.GetAllAsync();

        public async Task<Course?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

        public async Task CreateAsync(Course course)
        {
            await _repo.AddAsync(course);
            await _repo.SaveChangesAsync();
        }
    }
}