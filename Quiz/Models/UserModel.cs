using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lesson.Models;

namespace Authentication.Models
{
    public class User
    {
        public int? Id { get; set; }

        [Required]
        public string? Name { get; set; } // New property for user's full name

        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? PasswordHash { get; set; }

        [Required]
        public string? Role { get; set; } // Admin, Teacher, Student

        public bool? IsAdmin { get; set; } // Added this property

        public int? FacultyId { get; set; }
        public Faculty? Faculty { get; set; }

        public string? EducationType { get; set; } // "Bachelor", "Master", "Doctorate"

        // Subjects the user has access to (for students)
        public ICollection<Lesson.Models.Subject>? AccessibleSubjects { get; set; } = new List<Lesson.Models.Subject>();

        // Subjects the user teaches (for teachers)
        public ICollection<Lesson.Models.Subject>? TaughtSubjects { get; set; } = new List<Lesson.Models.Subject>();
    }

    public class Faculty
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public List<User>? Users { get; set; }
        public List<Specialization>? Specializations { get; set; }
    }


    public class Specialization
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? EducationType { get; set; }
        public int? FacultyId { get; set; }
        public Faculty? Faculty { get; set; }
        public ICollection<Subject>? Subjects { get; set; }
    }
}
