using Assesment.Application.DTOs;

namespace Assesment.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentDto>> GetAppointmentsForPatientAsync(string patientId);
        Task<AppointmentDto> BookAppointmentAsync(string patientId, CreateAppointmentRequest request);
        Task<bool> CancelAppointmentAsync(string patientId, int appointmentId);
    }
}