using System.Collections.Generic;

namespace Booking.Models
{
    public class BookingViewModel
    {
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
        public string BookingDate { get; set; }
        public string StartTime { get; set; }
        public int DepartmentId { get; set; }
        public int TeacherId { get; set; }
    }

    public class Teacher
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
    }

    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
