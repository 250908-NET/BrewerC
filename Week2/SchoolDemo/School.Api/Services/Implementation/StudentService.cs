using School.Models;
using School.Repositories;

namespace School.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;

        public StudentService(IStudentRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Student>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Student?> GetByIdAsync(int id) 
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task CreateAsync(Student student)
        {
            // the long-hand way
            await _repo.AddAsync(student);
        }

        // the short-hand way
        public async Task DeleteAsync(int id) 
        {
            await _repo.DeleteAsync(id);
        }
        
        public async Task UpdateAsync(int id, Student student) 
        {
            await _repo.UpdateAsync(id, student);
        }

        public async Task<bool> Exists(int id) => await _repo.Exists(id);

        public async Task EnrollAsync(int studentId, int courseId) => await _repo.EnrollAsync(studentId, courseId);
    }
}