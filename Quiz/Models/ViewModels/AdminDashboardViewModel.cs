using System.Collections.Generic;

namespace Authentication.Models
{
    public class AdminDashboardViewModel
    {
        public List<Specialization>? Specializations { get; set; }
        public List<Department>? Departments { get; set; }
    }
}
