using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class DoctorRepo
    {
        HospitalContext db;
        public DoctorRepo(HospitalContext db)
        {
            this.db = db;
        }

        public List<Doctor> Get()
        {
            return db.Doctors.ToList();
        }

        public Doctor Get(int id)
        {
            return db.Doctors.Find(id);
        }

        public List<Doctor> GetWithAppointments()
        {
            var data = db.Doctors
                .Include(d => d.Appointments)
                .ToList();
            return data;
        }

        public bool Create(Doctor d)
        {
            db.Doctors.Add(d);
            return db.SaveChanges() > 0;
        }

        public bool Update(Doctor d)
        {
            var exObj = db.Doctors.Find(d.Id);
            if (exObj == null) return false;
            exObj.Name = d.Name;
            exObj.Speciality = d.Speciality;
            exObj.Phone = d.Phone;
            exObj.Email = d.Email;
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var data = db.Doctors.Find(id);
            if (data == null) return false;
            db.Doctors.Remove(data);
            return db.SaveChanges() > 0;
        }
    }
}
