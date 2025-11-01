namespace Assesment.Domain.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Specialization { get; set; } = default!;
        public string Biography { get; set; } = default!;
        public string ProfilePictureUrl { get; set; } = default!;
        public string Availability { get; set; } = default!;
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}