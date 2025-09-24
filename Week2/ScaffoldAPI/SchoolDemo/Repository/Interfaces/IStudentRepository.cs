using School.Models;

namespace School.Repositories;

public interface IStudentRepository
{
    Task<List<Student>> GetAllAsync();
    Task<Student?> GetByIdAsync(id id);
    Task AddAsync(Student student);
    Task SaveChangesAsync();
}