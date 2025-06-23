namespace DCBS.Model
{
    public class Appointment
    {
        /// <summary>
        /// Getter and setter for appointment id
        /// </summary>
        public int AppointmentID { get; set; }

        /// <summary>
        /// Getter and setter for patient id
        /// </summary>
        public int PatientID { get; set; }

        /// <summary>
        /// Getter and setter for Doctor id.
        /// </summary>
        public int DoctorId { get; set; }

        /// <summary>
        /// Getter and setter for Date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Getter and setter for Department
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Getter and setter for appointment status
        /// </summary>
        public string Status { get; set; }

    }
}
