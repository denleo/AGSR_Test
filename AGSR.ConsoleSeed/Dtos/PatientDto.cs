namespace AGSR.ConsoleSeed.Dtos;

public record PatientDto : CreatePatientDto
{
    public Guid Id { get; init; }
}