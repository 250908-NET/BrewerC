using School.Models;

namespace School.Repositories
{
    public interface IInstructorRepository
    {
        public Task<List<Instructor>> GetAllAsync();
        public Task<Instructor?> GetByIdAsync(int id);
        public Task AddAsync(Instructor instructor);
        public Task UpdateAsync(int id, Instructor instructor);
        public Task DeleteAsync(int id);
        public Task<bool> Exists(int id);
    }
}