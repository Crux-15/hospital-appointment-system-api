namespace BLL.Models
{
    public class DoctorAppointmentModel : DoctorModel
    {
        public List<AppointmentModel> Appointments { get; set; } = new List<AppointmentModel>();
    }
}
