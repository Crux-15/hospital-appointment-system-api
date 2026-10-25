using AutoMapper;
using BLL.Models;
using DAL.EF.Tables;
using DAL.Repository;

namespace BLL.Services
{
    public class PatientService
    {
        PatientRepo? repo;
        IMapper? mapper;

        public PatientService() { }

        public PatientService(PatientRepo repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public virtual List<PatientModel> All()
        {
            var data = repo.Get();
            var mapped = mapper.Map<List<PatientModel>>(data);
            return mapped;
        }

        public virtual PatientModel? GetById(int id)
        {
            var data = repo.Get(id);
            var mapped = mapper.Map<PatientModel>(data);
            return mapped;
        }

        public virtual bool Create(PatientModel m)
        {
            var entity = mapper.Map<Patient>(m);
            return repo.Create(entity);
        }

        public virtual bool Update(PatientModel m)
        {
            var entity = mapper.Map<Patient>(m);
            return repo.Update(entity);
        }

        public virtual bool Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}
