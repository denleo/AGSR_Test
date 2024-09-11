using System.ComponentModel.DataAnnotations;

namespace AGSR.Services.Dto;

public record PatientDto : CreatePatientDto
{
    [Required] public Guid Id { get; init; }
}