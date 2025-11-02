using Assesment.Domain.Entities;
using Assesment.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Assesment.Application.DTOs;

namespace Assesment.Infrastructure.Services
{
    public class AppointmentService
    {
        private readonly ApplicationDbContext _context;

        public AppointmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Appointment>> GetUserAppointmentsAsync(string patientId)
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Where(a => a.PatientId == patientId)
                .OrderByDescending(a => a.DateTime)
                .ToListAsync();
        }

        public async Task<Appointment> CreateAsync(string patientId, string patientName, CreateAppointmentRequest request)
        {
            var doctor = await _context.Doctors.FindAsync(request.DoctorId);
            if (doctor == null)
                throw new ArgumentException("Doctor not found");

            var appointment = new Appointment
            {
                DoctorId = request.DoctorId,
                DateTime = request.DateTime,
                Description = request.Description,
                Specialization = request.Specialization ?? doctor.Specialization,
                PatientId = patientId,
                PatientName = patientName
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return appointment;
        }

        public async Task<bool> DeleteAsync(int appointmentId, string patientId)
        {
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.Id == appointmentId && a.PatientId == patientId);

            if (appointment == null)
                return false;

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
