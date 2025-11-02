using System.Collections.Generic;
using System.Threading.Tasks;
using Assesment.Domain.Entities;
using Assesment.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Assesment.Infrastructure.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly ApplicationDbContext _context;
        private readonly OpenAiService _openAiService;

        public RecommendationService(ApplicationDbContext context, OpenAiService openAiService)
        {
            _context = context;
            _openAiService = openAiService;
        }

        public async Task<IEnumerable<DoctorRecommendation>> RecommendDoctorsAsync(string symptoms)
        {
            var doctors = await _context.Doctors.ToListAsync();
            return await _openAiService.GetRecommendationsAsync(symptoms, doctors);
        }
    }
}
