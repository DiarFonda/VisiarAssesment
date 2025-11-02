using Microsoft.AspNetCore.Mvc;

namespace Assesment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenAiTestController : ControllerBase
    {
        [HttpGet("check")]
        public IActionResult CheckKey()
        {
            var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

            if (string.IsNullOrWhiteSpace(apiKey))
                return BadRequest("OPENAI_API_KEY is missing or not loaded from .env");

            return Ok("OpenAI API key loaded successfully!");
        }
    }
}
