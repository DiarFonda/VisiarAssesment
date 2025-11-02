using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assesment.Infrastructure.Services
{
    public interface IRecommendationService
    {
        Task<IEnumerable<DoctorRecommendation>> RecommendDoctorsAsync(string symptoms);
    }

    public class DoctorRecommendation
    {
        public int DoctorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public double Score { get; set; }
        public string MatchReason { get; set; } = string.Empty;
    }
}
