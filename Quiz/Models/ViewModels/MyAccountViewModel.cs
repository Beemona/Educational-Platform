namespace Authentication.Models
{
    public class MyAccountViewModel
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public Faculty Faculty { get; set; }
        public EducationType EducationType { get; set; }
        public Specialization Specialization { get; set; }
        public Department Department { get; set; }
    }
}
