namespace BLL.Models
{
    public class DoctorModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Speciality { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
