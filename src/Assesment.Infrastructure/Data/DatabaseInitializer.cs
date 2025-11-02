using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Assesment.Infrastructure.Identity; 
using Microsoft.AspNetCore.Identity; 
using Assesment.Domain.Entities;

namespace Assesment.Infrastructure.Data
{
    public static class DatabaseInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
            await context.Database.EnsureCreatedAsync();

            await ResetAndSeedDoctorsAsync(context);
        }

        private static async Task ResetAndSeedDoctorsAsync(ApplicationDbContext context)
        {
            var allDoctors = await context.Doctors.ToListAsync();
            context.Doctors.RemoveRange(allDoctors);

            var seedDoctors = SeedDoctors.GetInitialDoctors();
            await context.Doctors.AddRangeAsync(seedDoctors);

            await context.SaveChangesAsync();
        }
        private static async Task CreateTestUserAsync(UserManager<ApplicationUser> userManager)
        {
            var testEmail = "test@example.com";
            var existingUser = await userManager.FindByEmailAsync(testEmail);

         
            
                var testUser = new ApplicationUser
                {
                    Email = testEmail,
                    FullName = "Test Patient",
                };

                var result = await userManager.CreateAsync(testUser, "Test123!");

                if (result.Succeeded)
                {
                    Console.WriteLine("Test user created: test@example.com / Test123!");
                }
                else
                {
                    Console.WriteLine("Failed to create test user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            
        }
    }
}