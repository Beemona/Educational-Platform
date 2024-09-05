using Authentication.Models;

public class TeacherRegistrationViewModel : UserRegistrationViewModel
{
    public int? DepartmentId { get; set; }

    public List<Department>? Departments { get; set; } // Populated in controller
}
