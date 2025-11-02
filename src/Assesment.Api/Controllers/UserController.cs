using Assesment.Infrastructure.Data;
using Assesment.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Assesment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // only authenticated users
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/users/me
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            // Get the patient ID from JWT claims
            var patientId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(patientId))
                return Unauthorized("Patient ID not found in token");

            // Query the Patient entity
            var patient = await _context.Patients.FindAsync(patientId);

            if (patient == null)
                return NotFound("Patient not found");

            // Return only the relevant info
            return Ok(new
            {
                patient.Id,
                patient.FullName,
                patient.Email
            });
        }
    }
}
