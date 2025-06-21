using Microsoft.EntityFrameworkCore;
using DCBS.Model;

namespace DCBS.Data
{
    public class HospitalDbContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }

        public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>().ToTable("Appointments");
        }
        
    }
}
