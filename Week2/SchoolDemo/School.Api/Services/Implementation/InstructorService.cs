using School.Models;
using School.Repositories;

namespace School.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _repo;

        public InstructorService(IInstructorRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Instructor>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Instructor?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task CreateAsync(Instructor instructor)
        {
            await _repo.AddAsync(instructor);
        }

        public async Task UpdateAsync(int id, Instructor instructor) 
        {
            await _repo.UpdateAsync(id, instructor);
        }

        public async Task DeleteAsync(int id) 
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<bool> Exists(int id) => await _repo.Exists(id);
    }
}