
using Microsoft.EntityFrameworkCore;
using School.Models;

namespace School.Data;

public class SchoolDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Course> Courses { get; set; }

    public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
