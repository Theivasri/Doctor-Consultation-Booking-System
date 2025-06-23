using DCBS.Data;
using DCBS.Repositories;
using DCBS.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DCBS.Controllers
{
    public class PatientController : Controller
    {
        private readonly AppointmentRepository _repo;

        public PatientController(HospitalDbContext context)
        {
            _repo = new AppointmentRepository(context);
        }

        /// <summary>
        /// Appointment History
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult AppointmentOverview(int id)
        {
            var viewModel = new AppointmentOverviewViewModel
            {
                Upcoming = _repo.GetAppointmentsByStatus(id, "Upcoming"),
                Completed = _repo.GetAppointmentsByStatus(id, "Completed"),
                Cancelled = _repo.GetAppointmentsByStatus(id, "Cancelled")
            };

            return View(viewModel);
        }
    }
}