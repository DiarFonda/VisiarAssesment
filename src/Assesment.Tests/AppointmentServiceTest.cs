using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Assesment.Infrastructure.Services;
using Assesment.Infrastructure.Data;
using Assesment.Domain.Entities;
using Assesment.Application.DTOs;
using System.Linq;

namespace Assesment.Tests
{
    public class AppointmentServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly AppointmentService _service;

        public AppointmentServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);

            _context.Doctors.Add(new Doctor
            {
                Id = 1,
                FullName = "Dr. Alice",
                Specialization = "General",
                Availability = "Mon-Fri 9am-5pm", // required
                Biography = "Experienced general physician", // required
                ProfilePictureUrl = "https://example.com/profile.jpg" // required
            });
            _context.SaveChanges();

            _service = new AppointmentService(_context);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateAppointment()
        {
            var patientId = "patient-123";
            var patientName = "John Doe";

            var request = new CreateAppointmentRequest(
                DoctorId: 1,
                DateTime: DateTime.Now.AddDays(1),
                Description: "Feeling unwell",
                Specialization: "General"
            );

            var appointment = await _service.CreateAsync(patientId, patientName, request);

            Assert.NotNull(appointment);
            Assert.Equal(patientId, appointment.PatientId);
            Assert.Equal("Feeling unwell", appointment.Description);
            Assert.Equal(1, appointment.DoctorId);
        }

        [Fact]
        public async Task GetUserAppointmentsAsync_ShouldReturnAppointments()
        {
            var patientId = "patient-123";
            var patientName = "John Doe";

            var request = new CreateAppointmentRequest(
                DoctorId: 1,
                DateTime: DateTime.Now.AddDays(1),
                Description: "Feeling unwell",
                Specialization: "General"
            );

            var appointment = await _service.CreateAsync(patientId, patientName, request);

            var appointments = await _service.GetUserAppointmentsAsync(patientId);

            Assert.Single(appointments);
            Assert.Equal(appointment.Id, appointments.First().Id);
        }
    }
}
