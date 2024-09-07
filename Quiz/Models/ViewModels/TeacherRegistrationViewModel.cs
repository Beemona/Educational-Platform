using Authentication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

public class TeacherRegistrationViewModel : UserRegistrationViewModel
{
    public int? DepartmentId { get; set; }
    public List<Department>? Departments { get; set; }
}
