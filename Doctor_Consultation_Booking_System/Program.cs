using DCBS.Data;
using DCBS.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Register EF Core with SQLite
builder.Services.AddDbContext<HospitalDbContext>(options =>
    options.UseSqlite("Data Source=Appointment.db"));
builder.Services.AddScoped<AppointmentRepository>();


// ✅ Add both MVC views and API support
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// ✅ Support for [ApiController] endpoints (like /api/appointment/overview)
app.MapControllers();

// ✅ MVC default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Patient}/{action=AppointmentOverview}/{id=1}");

app.Run();