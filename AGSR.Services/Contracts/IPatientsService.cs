using AGSR.Services.Dto;

namespace AGSR.Services.Contracts;

public interface IPatientsService
{
    Task<PatientDto> CreateAsync(CreatePatientDto patient, CancellationToken token = default);
    Task<PatientDto> UpdateAsync(PatientDto patient, CancellationToken token = default);
    Task DeleteAsync(Guid patientId, CancellationToken token = default);
    Task<PatientDto?> GetByIdAsync(Guid patientId, CancellationToken token = default);
    Task<List<PatientDto>> GetAllAsync(string[] dateFilters, CancellationToken token = default);
}