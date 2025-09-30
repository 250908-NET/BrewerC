using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace School.Models;

public abstract class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string? FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public string? LastName { get; set; }

    [Required]
    public string? Email { get; set; }

    // UserType property for authentication/authorization
    [Required]
    public UserType UserType { get; set; }
}

public enum UserType
{
    SysAdmin = 0,
    Admin = 1,
    Teacher = 2,
    Student = 3
}