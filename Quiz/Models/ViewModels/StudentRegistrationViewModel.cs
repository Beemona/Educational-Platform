using Authentication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

public class StudentRegistrationViewModel : UserRegistrationViewModel
{
    // Use properties from the base class without hiding them
    public int? FacultyId { get; set; }
    public int? EducationTypeId { get; set; }
    public int? SpecializationId { get; set; }

    // Add additional properties specific to this view model
    public List<Faculty> Faculties { get; set; }
    public List<EducationType>? EducationTypes { get; set; }
    public List<Specialization>? Specializations { get; set; }
}
