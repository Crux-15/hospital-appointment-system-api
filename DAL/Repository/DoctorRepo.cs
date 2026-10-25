using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class DoctorRepo
    {
        HospitalContext? db;

        public DoctorRepo() { }

        public DoctorRepo(HospitalContext db)
        {
            this.db = db;
        }

        public virtual List<Doctor> Get()
        {
            return db.Doctors.ToList();
        }

        public virtual Doctor? Get(int id)
        {
            return db.Doctors.Find(id);
        }

        public virtual List<Doctor> GetWithAppointments()
        {
            var data = db.Doctors
                .Include(d => d.Appointments)
                .ToList();
            return data;
        }

        public virtual bool Create(Doctor d)
        {
            db.Doctors.Add(d);
            return db.SaveChanges() > 0;
        }

        public virtual bool Update(Doctor d)
        {
            var exObj = db.Doctors.Find(d.Id);
            if (exObj == null) return false;
            exObj.Name = d.Name;
            exObj.Speciality = d.Speciality;
            exObj.Phone = d.Phone;
            exObj.Email = d.Email;
            return db.SaveChanges() > 0;
        }

        public virtual bool Delete(int id)
        {
            var data = db.Doctors.Find(id);
            if (data == null) return false;
            db.Doctors.Remove(data);
            return db.SaveChanges() > 0;
        }
    }
}
