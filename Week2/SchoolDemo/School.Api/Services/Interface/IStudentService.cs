using School.Models;

namespace School.Services
{
    public interface IStudentService
    {
        public Task<List<Student>> GetAllAsync();
        public Task<Student?> GetByIdAsync(int id);
        public Task<Student> CreateAsync(Student student);
        public Task DeleteAsync(int id);
        public Task UpdateAsync(int id, Student student);
        public Task<bool> Exists(int id);
        public Task EnrollAsync(int studentId, int courseId);
    }
}