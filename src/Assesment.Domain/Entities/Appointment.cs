using System;

namespace Assesment.Domain.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public string PatientId { get; set; }   
        public string PatientName { get; set; } 

        public string Specialization { get; set; }
        public string Description { get; set; }
    }
}