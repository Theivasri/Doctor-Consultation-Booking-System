using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;
using DCBS.Model;
using DCBS.Repositories;
using DCBS.Model.StatusUpdateDto;

namespace DCBS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly string connectionString = "Data Source=Appointment.db";
        private readonly AppointmentRepository _repo;

        public AppointmentController(AppointmentRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("add")]
        public IActionResult AddAppointment([FromBody] Appointment appt)
        {
            using var conn = new SQLiteConnection(connectionString);
            conn.Open();

            string query = @"
                INSERT INTO Appointments 
                (PatientID, DoctorId, Date, Department, Status) 
                VALUES 
                (@PatientID, @DoctorId, @Date, @Department, @Status)";

            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@PatientID", appt.PatientID);
            cmd.Parameters.AddWithValue("@DoctorId", appt.DoctorId);
            cmd.Parameters.AddWithValue("@Date", appt.Date);
            cmd.Parameters.AddWithValue("@Department", appt.Department);
            cmd.Parameters.AddWithValue("@Status", appt.Status);
            cmd.ExecuteNonQuery();

            return Ok("Appointment added successfully.");
        }

        [HttpGet("overview/{id}")]
        public IActionResult GetAppointmentOverview(int id)
        {
            var upcoming = new List<Appointment>();
            var completed = new List<Appointment>();
            var cancelled = new List<Appointment>();

            using var conn = new SQLiteConnection(connectionString);
            conn.Open();

            string query = @"
                SELECT * 
                FROM Appointments 
                WHERE PatientID = @id 
                ORDER BY Date DESC";

            using var cmd = new SQLiteCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var appt = new Appointment
                {
                    AppointmentID = Convert.ToInt32(reader["AppointmentID"]),
                    PatientID = Convert.ToInt32(reader["PatientID"]),
                    DoctorId = Convert.ToInt32(reader["DoctorId"]),
                    Date = DateTime.Parse(reader["Date"].ToString()!),
                    Department = reader["Department"].ToString()!,
                    Status = reader["Status"].ToString()!
                };

                switch (appt.Status)
                {
                    case "Upcoming":
                        upcoming.Add(appt);
                        break;
                    case "Completed":
                        completed.Add(appt);
                        break;
                    case "Cancelled":
                        cancelled.Add(appt);
                        break;
                }
            }

            return Ok(new
            {
                Upcoming = upcoming,
                Completed = completed,
                Cancelled = cancelled
            });
        }

        [HttpPut("{appointmentId}/status")]
        public async Task<IActionResult> UpdateStatus(int appointmentId, [FromBody] StatusUpdateDto update)
        {
            if (string.IsNullOrWhiteSpace(update.NewStatus))
                return BadRequest("Status value is required.");

            await _repo.UpdateStatusAsync(appointmentId, update.NewStatus);
            return Ok("Status updated successfully.");
        }
    }
}