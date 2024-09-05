using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Authentication.Models;
using DocumentFormat.OpenXml.Bibliography;
using Lesson.Models;

namespace Authentication.Models
{
    public class User
    {
        public int? Id { get; set; }

        [Required]
        public string? Name { get; set; } 

        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? PasswordHash { get; set; }

        [Required]
        public string? Role { get; set; } // Admin, Teacher, Student

        public bool? IsAdmin { get; set; } 

    }

    public class Student : User
    {
        // Access to 1 EducationType, 1 Faculy, 1 Specialization, multiple AccessibleSubjects
        [Required]
        public int? FacultyId { get; set; }
        public Faculty? Faculty { get; set; }

        [Required]
        public int? EducationTypeId { get; set; } // Foreign key to EducationType
        public EducationType? EducationType { get; set; }

        [Required]
        public int? SpecializationId { get; set; }
        public Specialization? Specialization { get; set; }

        public ICollection<Subject>? AccessibleSubjects { get; set; } = new List<Subject>();
    }

    public class Teacher : User
    {
        // Access to 1 Department, multiple EducationType, multiple Faculies, multiple Specializations, multiple TaughtSubjects

        [Required]
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

        public ICollection<Faculty>? Faculties { get; set; } = new List<Faculty>();
        public ICollection<EducationType>? EducationTypes { get; set; } = new List<EducationType>();
        public ICollection<Specialization>? Specializations { get; set; } = new List<Specialization>();
        public ICollection<Subject>? TaughtSubjects { get; set; } = new List<Subject>();
    }

    public class Admin : User
    {
        public Admin()
        {
            IsAdmin = true; // Admins have this set to true
        }

        // Admins have access to everything
    }
    public class Department
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Teacher>? Teachers { get; set; }
        public ICollection<Specialization>? Specializations { get; set; } = new List<Specialization>();
    }

    public class Faculty
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Student>? Students { get; set; }
        public ICollection<Teacher>? Teachers { get; set; }
        public ICollection<Specialization>? Specializations { get; set; }
    }

    public class EducationType
    {
        public int? Id { get; set; }
        public string? Name { get; set; } // Values like "Bachelor", "Master", "Doctorate"

        public ICollection<Student>? Students { get; set; }
        public ICollection<Teacher>? Teachers { get; set; }
        public ICollection<Specialization>? Specializations { get; set; }
    }


    public class Specialization
    {
        public int? Id { get; set; }
        public string? Name { get; set; }

        // Foreign key to Department
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

        // Foreign key to Faculty
        public int? FacultyId { get; set; }
        public Faculty? Faculty { get; set; }

        // Foreign key to EducationType
        public int? EducationTypeId { get; set; }
        public EducationType? EducationType { get; set; }

        // Collection of Subjects who are associated with this Specialization
        public ICollection<Subject>? Subjects { get; set; }

        // Collection of Teachers who are associated with this Specialization
        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();

        // Collection of Students who are associated with this Specialization
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }

}