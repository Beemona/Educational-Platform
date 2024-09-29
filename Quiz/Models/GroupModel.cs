using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Authentication.Models;

namespace Quiz.Models
{
    public class Group
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string GroupType { get; set; } // e.g., "class", "project"

        public List<Student> Students { get; set; } = new List<Student>();
    }
}
