using Assesment.Application.DTOs;
using Assesment.Application.Interfaces;
using Assesment.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assesment.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Patient> _userManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<Patient> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            var user = new Patient { Email = request.Email, FullName = request.FullName };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                throw new ApplicationException(string.Join(", ", result.Errors.Select(e => e.Description)));

            return await GenerateJwtToken(user);
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                throw new UnauthorizedAccessException("Invalid credentials");

            return await GenerateJwtToken(user);
        }

        private Task<AuthResponse> GenerateJwtToken(Patient user)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Name, user.Email ?? string.Empty),
        new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);

            return Task.FromResult(new AuthResponse(
                new JwtSecurityTokenHandler().WriteToken(token),
                user.Email!
            ));
        }
    };
};