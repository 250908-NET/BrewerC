using School.Models;

namespace School.Repositories
{
    public interface IInstructorRepository
    {
        public async Task<List<Instructor>> GetAllAsync();
        public async Task<Instructor?> GetByIdAsync(int id);
        public async Task AddAsync(Instructor instructor);
        public async Task SaveChangesAsync();
    }
}