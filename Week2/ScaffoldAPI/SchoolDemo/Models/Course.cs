using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Models;

public class Course
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required, MaxLength(32)]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public Instructor instructor { get; set; }
    public ICollection<Student> Students { get; set; }
}