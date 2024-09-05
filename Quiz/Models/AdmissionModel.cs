using System.Collections.Generic;

namespace Admission.Model
{
    public class AdmissionViewModel
    {
        // Properties for the form fields
        public string? SelectedFaculty { get; set; }
        public string? SelectedDegree { get; set; }
        public string? SelectedProgram { get; set; }
        public string? LearningType { get; set; }
        public string? ApplicationType { get; set; }

        // Options for dropdowns
        public List<string>? Faculties { get; set; }
        public List<string>? Degrees { get; set; }
        public List<string>? Programs { get; set; }
        public List<string>? LearningTypes { get; set; }
        public List<string>? ApplicationTypes { get; set; }
    }
}