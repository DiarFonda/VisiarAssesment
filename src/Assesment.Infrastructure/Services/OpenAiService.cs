using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Assesment.Domain.Entities;

namespace Assesment.Infrastructure.Services
{
    public class OpenAiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<OpenAiService> _logger;
        private readonly string _apiKey;

        public OpenAiService(HttpClient httpClient, IConfiguration configuration, ILogger<OpenAiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _apiKey = configuration["AppSettings:OpenAiApiKey"] ?? throw new Exception("OpenAI API key not found");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        }

        public async Task<List<DoctorRecommendation>> GetRecommendationsAsync(string symptoms, List<Doctor> doctors)
        {
            if (doctors.Count == 0)
                return new List<DoctorRecommendation>();

            var specializationList = string.Join(", ", doctors.ConvertAll(d => d.Specialization));

            var prompt = $@"
You are a medical triage assistant that maps symptoms to doctor specializations.
Analyze the symptoms and choose one or more specializations from this list:
[{specializationList}]

Respond strictly in this JSON format:
{{
  ""specializations"": [
    {{""name"": ""Cardiology"", ""confidence"": 0.9, ""reason"": ""Heart-related symptoms detected""}}
  ]
}}

Symptoms: {symptoms}
";

            var requestBody = new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
                    new { role = "system", content = "You are a helpful medical triage assistant. Respond ONLY with valid JSON and nothing else." },
                    new { role = "user", content = prompt }
                },
                temperature = 0.2
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestBody);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("OpenAI raw response: {Response}", json);
                using var root = JsonDocument.Parse(json);
                var content = root.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
                _logger.LogInformation("AI message content: {Content}", content);
                if (string.IsNullOrWhiteSpace(content))
                    return new List<DoctorRecommendation>();

                List<DoctorRecommendation> results = new();

                try
                {
                    using var parsed = JsonDocument.Parse(content);
                    if (parsed.RootElement.TryGetProperty("specializations", out var specs))
                    {
                        foreach (var spec in specs.EnumerateArray())
                        {
                            var name = spec.GetProperty("name").GetString() ?? "";
                            var confidence = spec.GetProperty("confidence").GetDouble();
                            var reason = spec.GetProperty("reason").GetString() ?? "";

                            foreach (var doctor in doctors)
                            {
                                if (doctor.Specialization.Contains(name, StringComparison.OrdinalIgnoreCase))
                                {
                                    results.Add(new DoctorRecommendation
                                    {
                                        DoctorId = doctor.Id,
                                        Name = doctor.FullName,
                                        Specialization = doctor.Specialization,
                                        Score = confidence,
                                        MatchReason = reason
                                    });
                                }
                            }
                        }
                    }
                }
                catch (Exception inner)
                {
                    _logger.LogError(inner, "Failed to parse AI JSON content: {Content}", content);
                }

                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get AI recommendations");
                return new List<DoctorRecommendation>();
            }
        }
    }
}
