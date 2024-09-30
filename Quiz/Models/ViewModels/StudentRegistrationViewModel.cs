using Authentication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

public class StudentRegistrationViewModel : UserRegistrationViewModel
{
    // Use properties from the base class without hiding them
    //public int? FacultyId { get; set; }
    //public int? EducationTypeId { get; set; }
    //public int? SpecializationId { get; set; }

    // Add additional properties specific to this view model
    public new List<Faculty> Faculties { get; set; } = new List<Faculty>(); // Instantiate to avoid null reference
    public new List<EducationType>? EducationTypes { get; set; } = new List<EducationType>(); // Instantiate to avoid null reference
    public new List<Specialization>? Specializations { get; set; } = new List<Specialization>(); // Instantiate to avoid null reference
}

