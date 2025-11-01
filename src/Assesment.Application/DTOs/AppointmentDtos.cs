namespace Assesment.Application.DTOs
{

    public record CreateAppointmentRequest(
        int DoctorId,
        DateTime DateTime,
        string Description,
        string Specialization
    );

    public record AppointmentDto(
        int Id,
        DateTime DateTime,
        int DoctorId,
        string DoctorName,
        string DoctorSpecialization,
        string PatientId,
        string PatientName,
        string Specialization,
        string Description
    );
}