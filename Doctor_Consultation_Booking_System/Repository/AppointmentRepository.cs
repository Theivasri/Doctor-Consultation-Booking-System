using DCBS.Model;
using DCBS.Data;
using System.Data.Entity;

namespace DCBS.Repositories
{
    public class AppointmentRepository
    {
        private readonly HospitalDbContext _context;
        public AppointmentRepository(HospitalDbContext context)
        {
            _context = context;
        }
        public void AddAppointment(Appointment appt)
        {
            _context.Appointments.Add(appt);
            _context.SaveChanges();
        }

        public List<Appointment> GetAppointmentsByStatus(int patientId, string status)
        {
            return _context.Appointments
                .Include(a => a.DoctorId) // 💡 This is the key addition
                .Where(a => a.PatientID == patientId && a.Status == status)
                .OrderByDescending(a => a.Date)
                .ToList();
        }

        public List<Appointment> GetAppointmentsByStatus(int patientId, int DoctorId, string status)
        {
            return _context.Appointments
                .Include(a => a.DoctorId) // 💡 This is the key addition
                .Where(a => a.PatientID == patientId && a.Status == status)
                .OrderByDescending(a => a.Date)
                .ToList();
        }

        public async Task UpdateStatusAsync(int appointmentId, string newStatus)
        {
            var appt = await _context.Appointments.FindAsync(appointmentId);
            if (appt != null)
            {
                appt.Status = newStatus;
                await _context.SaveChangesAsync();
            }
        }
    }
}