using School.Models;

namespace School.Repositories
{
    public interface ICourseRepository
    {
        public async Task<List<Course>> GetAllAsync();
        public async Task<Course?> GetByIdAsync(int id);
        public async Task AddAsync(Course course);
        public async Task SaveChangesAsync();
    }
}