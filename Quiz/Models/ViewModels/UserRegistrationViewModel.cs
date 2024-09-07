using Authentication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

public class UserRegistrationViewModel
{
    // General User Information
    public string? Role { get; set; } // Role selected: Student, Teacher, Admin
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Password { get; set; }

    // Student-Specific Fields
    public int? FacultyId { get; set; }
    public int? EducationTypeId { get; set; }
    public int? SpecializationId { get; set; }

    // Teacher-Specific Fields
    public int? DepartmentId { get; set; }

    // Data lists for dropdowns
    public List<Faculty>? Faculties { get; set; }
    public List<EducationType>? EducationTypes { get; set; }
    public List<Specialization>? Specializations { get; set; }
    public List<Department>? Departments { get; set; }
}
