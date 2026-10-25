using AutoMapper;
using BLL.Models;
using DAL.EF.Tables;
using DAL.Repository;

namespace BLL.Services
{
    public class DoctorService
    {
        DoctorRepo repo;
        IMapper mapper;
        public DoctorService(DoctorRepo repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public List<DoctorModel> All()
        {
            var data = repo.Get();
            var mapped = mapper.Map<List<DoctorModel>>(data);
            return mapped;
        }

        public DoctorModel GetById(int id)
        {
            var data = repo.Get(id);
            var mapped = mapper.Map<DoctorModel>(data);
            return mapped;
        }

        public List<DoctorAppointmentModel> GetWithAppointments()
        {
            var data = repo.GetWithAppointments();
            var mapped = mapper.Map<List<DoctorAppointmentModel>>(data);
            return mapped;
        }

        public List<DoctorInfoModel> GetDoctorInfos()
        {
            var data = repo.GetWithAppointments();
            var mapped = mapper.Map<List<DoctorInfoModel>>(data);
            return mapped;
        }

        public bool Create(DoctorModel m)
        {
            var entity = mapper.Map<Doctor>(m);
            return repo.Create(entity);
        }

        public bool Update(DoctorModel m)
        {
            var entity = mapper.Map<Doctor>(m);
            return repo.Update(entity);
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}
