using Microsoft.EntityFrameworkCore;
using DCBS.Model;
using DCBS.Model.Doctor;

namespace DCBS.Data
{
    public class HospitalDbContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>().ToTable("Appointments");
        }
        
    }
}
