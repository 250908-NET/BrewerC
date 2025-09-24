using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Models;

public class Instructor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required, MaxLength(32)]
    public string FirstName { get; set; }
    [Required, MaxLength(32)]
    public string LastName { get; set; }
    public ICollection<Course> Courses { get; set; }
}