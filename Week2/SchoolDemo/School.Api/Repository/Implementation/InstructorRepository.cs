using School.Models;
using School.Data;
using Microsoft.EntityFrameworkCore;

namespace School.Repositories
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly SchoolDbContext _context;

        public InstructorRepository(SchoolDbContext context) => _context = context;

        public async Task<List<Instructor>> GetAllAsync() => await _context.Instructors.Include(e => e.Courses).ToListAsync();

        public async Task<Instructor?> GetByIdAsync(int id) => await _context.Instructors.Include(e => e.Courses).FirstOrDefaultAsync(e => e.Id == id);

        public async Task AddAsync(Instructor instructor) 
        {
            await _context.Instructors.AddAsync(instructor);
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(int id, Instructor instructor)
        {
            _context.Instructors.Update(instructor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id) => await _context.Instructors.AnyAsync(e => e.Id == id);
    }
}
