using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Authentication.Models; // Adjust to your actual namespace
using Quiz.Models;

namespace Quiz.Controllers
{
    public class GroupsController : Controller
    {
        private static List<Group> _groups = new List<Group>();

        // Sample data for demonstration - List of Students
        private static List<Student> _students = new List<Student>
        {
            new Student
            {
                Id = 1,
                Name = "John Smith",
                Faculty = new Faculty { Name = "Faculty of Psychology" },
                EducationType = new EducationType { Name = "Master" },
                Specialization = new Specialization { Name = "Master in Forensic Psychology" }
            },
            new Student
            {
                Id = 2,
                Name = "Jane Doe",
                Faculty = new Faculty { Name = "Faculty of Engineering" },
                EducationType = new EducationType { Name = "Bachelor" },
                Specialization = new Specialization { Name = "Computer Science" }
            },
            new Student
            {
                Id = 3,
                Name = "Alice Johnson",
                Faculty = new Faculty { Name = "Faculty of Medicine" },
                EducationType = new EducationType { Name = "Bachelor" },
                Specialization = new Specialization { Name = "General Medicine" }
            },
            new Student
            {
                Id = 4,
                Name = "Bob Brown",
                Faculty = new Faculty { Name = "Faculty of Business" },
                EducationType = new EducationType { Name = "Bachelor" },
                Specialization = new Specialization { Name = "Business Administration" }
            },
        };

        public IActionResult Index()
        {
            ViewBag.Students = _students;
            return View(_groups);
        }

        [HttpPost]
        public IActionResult CreateGroup(string groupName, string groupType, List<int> selectedStudents)
        {
            // Validate inputs
            if (string.IsNullOrEmpty(groupName) || string.IsNullOrEmpty(groupType) || selectedStudents == null || !selectedStudents.Any())
            {
                return BadRequest("Group name, type, and students selection are required.");
            }

            // Check the group type and validate the size
            int minCount, maxCount;
            switch (groupType.ToLower())
            {
                case "class":
                    minCount = 28;
                    maxCount = 32;
                    break;
                case "project":
                    minCount = 2;
                    maxCount = 4;
                    break;
                default:
                    return BadRequest("Invalid group type.");
            }

            // Get the selected students directly from the selectedStudents list
            var studentsInGroup = _students
               .Where(s => selectedStudents.Contains(s.Id.Value)) // Check if the student's ID is in the selected IDs
               .ToList();

            // Validate student count
            if (studentsInGroup.Count < minCount || studentsInGroup.Count > maxCount)
            {
                return BadRequest($"The number of students must be between {minCount} and {maxCount} for the selected group type.");
            }

            // Create the group
            var group = new Group
            {
                Id = _groups.Count + 1, // Simple ID assignment
                Name = groupName,
                GroupType = groupType,
                Students = studentsInGroup // Assign the selected students
            };

            _groups.Add(group); // Add the new group to the static list of groups

            return RedirectToAction("Index"); // Redirect to the Index action
        }


        // Action to display the list of created groups
        public IActionResult GroupList()
        {
            return View(_groups);
        }
    }
}

