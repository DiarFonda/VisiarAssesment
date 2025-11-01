using Assesment.Domain.Entities;

namespace Assesment.Infrastructure.Data
{
    public static class SeedDoctors
    {
        public static Doctor[] GetInitialDoctors()
        {
            return new Doctor[]
            {
                new Doctor
                {
                    Id = 1,
                    FullName = "Dr. John Smith",
                    Specialization = "Cardiology",
                    Biography = "Experienced cardiologist with 15 years of practice. Specializes in heart disease prevention and treatment.",
                    ProfilePictureUrl = "https://images.unsplash.com/photo-1612349317150-e413f6a5b16d?w=400&h=400&fit=crop&crop=face",
                    Availability = "Monday-Friday, 9:00 AM - 5:00 PM"
                },
                new Doctor
                {
                    Id = 2,
                    FullName = "Dr. Sarah Johnson",
                    Specialization = "Pediatrics",
                    Biography = "Pediatric specialist focused on child healthcare and developmental milestones.",
                    ProfilePictureUrl = "https://images.unsplash.com/photo-1559839734-2b71ea197ec2?w=400&h=400&fit=crop&crop=face",
                    Availability = "Monday-Wednesday, 8:00 AM - 4:00 PM"
                },
                new Doctor
                {
                    Id = 3,
                    FullName = "Dr. Ahmed Karim",
                    Specialization = "Dermatology",
                    Biography = "Dermatologist specializing in skin conditions, acne treatment, and cosmetic dermatology.",
                    ProfilePictureUrl = "https://images.unsplash.com/photo-1582750433449-648ed127bb54?w=400&h=400&fit=crop&crop=face",
                    Availability = "Tuesday-Thursday, 10:00 AM - 6:00 PM"
                },
                new Doctor
                {
                    Id = 4,
                    FullName = "Dr. Maria Rodriguez",
                    Specialization = "Neurology",
                    Biography = "Neurologist with expertise in migraine treatment and neurological disorders.",
                    ProfilePictureUrl = "https://images.unsplash.com/photo-1594824947933-d0501ba2fe65?w=400&h=400&fit=crop&crop=face",
                    Availability = "Monday-Thursday, 8:30 AM - 4:30 PM"
                },
                new Doctor
                {
                    Id = 5,
                    FullName = "Dr. James Wilson",
                    Specialization = "Orthopedics",
                    Biography = "Orthopedic surgeon specializing in sports injuries and joint replacement surgeries.",
                    ProfilePictureUrl = "https://images.unsplash.com/photo-1622253692010-333f2da6031d?w=400&h=400&fit=crop&crop=face",
                    Availability = "Monday-Friday, 7:00 AM - 3:00 PM"
                },
                new Doctor
                {
                    Id = 6,
                    FullName = "Dr. Lisa Chen",
                    Specialization = "Psychiatry",
                    Biography = "Psychiatrist focusing on anxiety disorders, depression, and cognitive behavioral therapy.",
                    ProfilePictureUrl = "https://images.unsplash.com/photo-1591604021695-0c69b7c05981?w=400&h=400&fit=crop&crop=face",
                    Availability = "Tuesday-Friday, 9:00 AM - 5:00 PM"
                },
                new Doctor
                {
                    Id = 7,
                    FullName = "Dr. Robert Brown",
                    Specialization = "Gastroenterology",
                    Biography = "Gastroenterologist with expertise in digestive disorders and endoscopic procedures.",
                    ProfilePictureUrl = "https://images.unsplash.com/photo-1537368910025-700350fe46c7?w=400&h=400&fit=crop&crop=face",
                    Availability = "Monday-Wednesday-Friday, 8:00 AM - 4:00 PM"
                },
                new Doctor
                {
                    Id = 8,
                    FullName = "Dr. Emily Davis",
                    Specialization = "Obstetrics & Gynecology",
                    Biography = "OB/GYN providing comprehensive women's health services and prenatal care.",
                    ProfilePictureUrl = "https://images.unsplash.com/photo-1551601651-2a8555f1a136?w=400&h=400&fit=crop&crop=face",
                    Availability = "Monday-Thursday, 9:00 AM - 5:00 PM"
                }
            };
        }
    }
}