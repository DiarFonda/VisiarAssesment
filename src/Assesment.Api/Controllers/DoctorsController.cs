using Microsoft.AspNetCore.Mvc;
using Assesment.Domain.Entities;
using Assesment.Infrastructure.Data;
using Assesment.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assesment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly OpenAiService _recommendationService; 

        public DoctorsController(ApplicationDbContext context, OpenAiService recommendationService)
        {
            _context = context;
            _recommendationService = recommendationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            return await _context.Doctors.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
                return NotFound();

            return doctor;
        }

        [HttpPost("recommend")]
        public async Task<IActionResult> RecommendDoctors([FromBody] RecommendRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Symptoms))
                return BadRequest("Symptoms are required.");

            var doctors = await _context.Doctors.ToListAsync();
            var recommendations = await _recommendationService.GetRecommendationsAsync(request.Symptoms, doctors);

            return Ok(recommendations);
        }

        public class RecommendRequest
        {
            public string Symptoms { get; set; } = string.Empty;
        }
    }
}
