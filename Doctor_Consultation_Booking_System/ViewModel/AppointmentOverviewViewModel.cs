using DCBS.Model;

namespace DCBS.ViewModel;

public class AppointmentOverviewViewModel
{
    /// <summary>
    /// Getter and setter for patient id.
    /// </summary>
    public int PatientID { get; set; }
    
    /// <summary>
    /// List of upcoming appointments
    // </summary>
    public List<Appointment> Upcoming { get; set; }

    /// <summary>
    /// List of completed appointments
    /// </summary>
    public List<Appointment> Completed { get; set; }

    /// <summary>
    /// List of cancelled appointments
    /// </summary>
    public List<Appointment> Cancelled { get; set; }
}
