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

        public List<Appointment> GetAppointmentsByPatient(int patientId)
        {
            return _context.Appointments
                .Where(a => a.PatientID == patientId)
                .OrderByDescending(a => a.Date)
                .ToList();
        }

        public List<Appointment> GetAppointmentsByStatus(int patientId, string status)
        {
            return _context.Appointments
                .Where(a => a.PatientID == patientId && a.Status == status)
                .OrderByDescending(a => a.Date)
                .ToList();
        }

        public void AddAppointment(Appointment appt)
        {
            _context.Appointments.Add(appt);
            _context.SaveChanges();
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
        // public async Task AutoCompletePastAppointmentsAsync()
        // {
        //     var now = DateTime.Now;
        //     var expired = await _context.Appointments
        //         .Where(a => a.Status == "Upcoming" && a.Date < now)
        //         .ToListAsync();

        //     foreach (var appt in expired)
        //     {
        //         appt.Status = "Completed";
        //     }

        //     await _context.SaveChangesAsync();
        // }
    }
}