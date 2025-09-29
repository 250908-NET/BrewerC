using School.Models;

namespace School.Repositories
{
    public interface IStudentRepository
    {
        public Task<List<Student>> GetAllAsync();
        public Task<Student?> GetByIdAsync(int id);
        public Task<Student> AddAsync(Student student);
        public Task UpdateAsync(int id, Student student);
        public Task DeleteAsync(int id);
        public Task<bool> Exists(int id);
        public Task EnrollAsync(int studentId, int courseId);
    }
}