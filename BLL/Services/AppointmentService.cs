using AutoMapper;
using BLL.Models;
using DAL.EF.Tables;
using DAL.Repository;

namespace BLL.Services
{
    public class AppointmentService
    {
        AppointmentRepo repo;
        IMapper mapper;
        public AppointmentService(AppointmentRepo repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public List<AppointmentModel> All()
        {
            var data = repo.Get();
            var mapped = mapper.Map<List<AppointmentModel>>(data);
            return mapped;
        }

        public AppointmentModel GetById(int id)
        {
            var data = repo.Get(id);
            var mapped = mapper.Map<AppointmentModel>(data);
            return mapped;
        }

        public List<AppointmentModel> GetByDoctor(int doctorId)
        {
            var data = repo.GetByDoctor(doctorId);
            var mapped = mapper.Map<List<AppointmentModel>>(data);
            return mapped;
        }

        public List<AppointmentModel> GetByPatient(int patientId)
        {
            var data = repo.GetByPatient(patientId);
            var mapped = mapper.Map<List<AppointmentModel>>(data);
            return mapped;
        }

        public List<AppointmentModel> GetPending()
        {
            var data = repo.GetPending();
            var mapped = mapper.Map<List<AppointmentModel>>(data);
            return mapped;
        }

        public List<AppointmentModel> GetToday()
        {
            var data = repo.GetToday();
            var mapped = mapper.Map<List<AppointmentModel>>(data);
            return mapped;
        }

        public bool Create(AppointmentModel m)
        {
            var entity = mapper.Map<Appointment>(m);
            return repo.Create(entity);
        }

        public bool Update(AppointmentModel m)
        {
            var entity = mapper.Map<Appointment>(m);
            return repo.Update(entity);
        }

        public bool UpdateStatus(int id, string status)
        {
            return repo.UpdateStatus(id, status);
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}
