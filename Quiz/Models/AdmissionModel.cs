using System.Collections.Generic;

namespace Admission.Model
{
    public class AdmissionViewModel
    {
        public List<Faculty> Faculties { get; set; }
        public List<Specialization> Specializations { get; set; }
        public List<EducationType> EducationTypes { get; set; }
        public List<string>? ApplicationTypes { get; set; } // Add this line
        public List<string>? LearningTypes { get; set; }   // Add this line

        public AdmissionViewModel()
        {
            Faculties = new List<Faculty>();
            Specializations = new List<Specialization>();
            EducationTypes = new List<EducationType>();
        }

        public class Faculty
        {
            public int? Id { get; set; }
            public string? Name { get; set; }
        }

        public class Specialization
        {
            public int? Id { get; set; }
            public string? Name { get; set; }
            public int? FacultyId { get; set; }
            public int? EducationTypeId { get; set; }
        }

        public class EducationType
        {
            public int? Id { get; set; }
            public string? Name { get; set; }
        }
    }
}
