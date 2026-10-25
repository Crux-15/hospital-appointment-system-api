using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class AppointmentRepo
    {
        HospitalContext db;
        public AppointmentRepo(HospitalContext db)
        {
            this.db = db;
        }

        public List<Appointment> Get()
        {
            var data = db.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .ToList();
            return data;
        }

        public Appointment? Get(int id)
        {
            var data = db.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefault(a => a.Id == id);
            return data;
        }

        public List<Appointment> GetByDoctor(int doctorId)
        {
            var data = (from a in db.Appointments
                        where a.DoctorId == doctorId
                        select a).Include(a => a.Patient).ToList();
            return data;
        }

        public List<Appointment> GetByPatient(int patientId)
        {
            var data = (from a in db.Appointments
                        where a.PatientId == patientId
                        select a).Include(a => a.Doctor).ToList();
            return data;
        }

        public List<Appointment> GetPending()
        {
            var data = (from a in db.Appointments
                        where a.Status == "Pending"
                        select a).Include(a => a.Doctor).Include(a => a.Patient).ToList();
            return data;
        }

        public List<Appointment> GetToday()
        {
            var today = DateTime.Today;
            var data = (from a in db.Appointments
                        where a.AppointmentDate.Date == today
                        select a).Include(a => a.Doctor).Include(a => a.Patient).ToList();
            return data;
        }

        public bool Create(Appointment a)
        {
            db.Appointments.Add(a);
            return db.SaveChanges() > 0;
        }

        public bool UpdateStatus(int id, string status)
        {
            var exObj = db.Appointments.Find(id);
            if (exObj == null) return false;
            exObj.Status = status;
            return db.SaveChanges() > 0;
        }

        public bool Update(Appointment a)
        {
            var exObj = db.Appointments.Find(a.Id);
            if (exObj == null) return false;
            exObj.DoctorId = a.DoctorId;
            exObj.PatientId = a.PatientId;
            exObj.AppointmentDate = a.AppointmentDate;
            exObj.Status = a.Status;
            exObj.Notes = a.Notes;
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var data = db.Appointments.Find(id);
            if (data == null) return false;
            db.Appointments.Remove(data);
            return db.SaveChanges() > 0;
        }
    }
}
