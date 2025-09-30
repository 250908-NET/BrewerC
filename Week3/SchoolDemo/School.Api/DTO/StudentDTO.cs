using School.Models;

namespace School.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }
        public List<CourseDTO> Courses { get; set; } = new();
    }
}