using School.Models;

namespace School.Repositories
{
    public interface IStudentRepository
    {
        public async Task<List<Student>> GetAllAsync();
        public async Task<Student?> GetByIdAsync(int id);
        public async Task AddAsync(Student student);
        public async Task SaveChangesAsync();
    }
}