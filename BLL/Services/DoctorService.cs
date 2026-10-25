using AutoMapper;
using BLL.Models;
using DAL.EF.Tables;
using DAL.Repository;

namespace BLL.Services
{
    public class DoctorService
    {
        DoctorRepo? repo;
        IMapper? mapper;

        public DoctorService() { }

        public DoctorService(DoctorRepo repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public virtual List<DoctorModel> All()
        {
            var data = repo.Get();
            var mapped = mapper.Map<List<DoctorModel>>(data);
            return mapped;
        }

        public virtual DoctorModel? GetById(int id)
        {
            var data = repo.Get(id);
            var mapped = mapper.Map<DoctorModel>(data);
            return mapped;
        }

        public virtual List<DoctorAppointmentModel> GetWithAppointments()
        {
            var data = repo.GetWithAppointments();
            var mapped = mapper.Map<List<DoctorAppointmentModel>>(data);
            return mapped;
        }

        public virtual List<DoctorInfoModel> GetDoctorInfos()
        {
            var data = repo.GetWithAppointments();
            var mapped = mapper.Map<List<DoctorInfoModel>>(data);
            return mapped;
        }

        public virtual bool Create(DoctorModel m)
        {
            var entity = mapper.Map<Doctor>(m);
            return repo.Create(entity);
        }

        public virtual bool Update(DoctorModel m)
        {
            var entity = mapper.Map<Doctor>(m);
            return repo.Update(entity);
        }

        public virtual bool Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}
