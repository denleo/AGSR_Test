using System.ComponentModel.DataAnnotations;
using AGSR.Domain.Enums;

namespace AGSR.Services.Dto;

public record CreatePatientDto
{
    public HumanNameDto Name { get; init; } = null!;
    public Gender Gender { get; init; } = Gender.Unknown;
    [Required] public DateTime BirthDate { get; init; }
    public bool Active { get; init; } = true;
}