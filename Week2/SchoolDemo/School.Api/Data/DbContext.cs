using Microsoft.EntityFrameworkCore;
using School.Models;

namespace School.Data;

public class SchoolDbContext : DbContext
{
    // Fields
    public DbSet<Student> Students { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Course> Courses { get; set; }
    
    // Methods
    // You still need a constructor!

    public SchoolDbContext( DbContextOptions<SchoolDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Course configuration
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);
                
            entity.Property(e => e.Description)
                .HasMaxLength(1000);
                
            entity.HasOne(e => e.Instructor)
                .WithMany(i => i.Courses)
                .HasForeignKey(e => e.InstructorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        });

           // Student-Course many-to-many relationship
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasMany(s => s.Courses)
            .WithMany(c => c.Students)
            .UsingEntity<Dictionary<string, object>>(
                "StudentCourse",  // Junction table name
                j => j.HasOne<Course>().WithMany().HasForeignKey("CourseId"),
                j => j.HasOne<Student>().WithMany().HasForeignKey("StudentId"),
                j =>
                {
                    j.HasKey("StudentId", "CourseId");
                    j.ToTable("StudentCourses");
                });
        });
    
        base.OnModelCreating(modelBuilder);
    }
}