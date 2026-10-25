using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class PatientRepo
    {
        HospitalContext? db;

        public PatientRepo() { }

        public PatientRepo(HospitalContext db)
        {
            this.db = db;
        }

        public virtual List<Patient> Get()
        {
            return db.Patients.ToList();
        }

        public virtual Patient? Get(int id)
        {
            return db.Patients.Find(id);
        }

        public virtual bool Create(Patient p)
        {
            db.Patients.Add(p);
            return db.SaveChanges() > 0;
        }

        public virtual bool Update(Patient p)
        {
            var exObj = db.Patients.Find(p.Id);
            if (exObj == null) return false;
            exObj.Name = p.Name;
            exObj.Age = p.Age;
            exObj.Gender = p.Gender;
            exObj.Phone = p.Phone;
            exObj.Email = p.Email;
            return db.SaveChanges() > 0;
        }

        public virtual bool Delete(int id)
        {
            var data = db.Patients.Find(id);
            if (data == null) return false;
            db.Patients.Remove(data);
            return db.SaveChanges() > 0;
        }
    }
}
