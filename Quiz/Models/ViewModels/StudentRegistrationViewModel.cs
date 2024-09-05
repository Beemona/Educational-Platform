using Authentication.Models;

public class StudentRegistrationViewModel : UserRegistrationViewModel
{
    public int? FacultyId { get; set; }
    public string? EducationType { get; set; }
    public int? SpecializationId { get; set; }

    public List<Faculty>? Faculties { get; set; } // Populated in controller
    public List<Specialization>? Specializations { get; set; } // Populated based on FacultyId
}
