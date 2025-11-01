using Assesment.Infrastructure.Services;
using Assesment.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Assesment.Application.DTOs;

namespace Assesment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppointmentService _appointmentService;

        public AppointmentsController(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserAppointments()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token.");

            var appointments = await _appointmentService.GetUserAppointmentsAsync(userId);
            return Ok(appointments);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentRequest request)
        {
            if (request == null)
                return BadRequest("Invalid appointment request.");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token.");

            var appointment = await _appointmentService.CreateAsync(userId, userName, request);
            return Created($"/api/appointments/{appointment.Id}", appointment);
        }
    }
}
