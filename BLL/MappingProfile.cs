using AutoMapper;
using BLL.Models;
using DAL.EF.Tables;

namespace BLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Doctor, DoctorModel>().ReverseMap();
            CreateMap<Doctor, DoctorAppointmentModel>();
            CreateMap<Doctor, DoctorInfoModel>()
                .ForMember(d => d.TotalAppointments,
                           s => s.MapFrom(s => s.Appointments.Count));

            CreateMap<Patient, PatientModel>().ReverseMap();

            CreateMap<Appointment, AppointmentModel>().ReverseMap();
        }
    }
}
