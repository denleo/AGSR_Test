namespace AGSR.ConsoleSeed.Dtos;

public record CreatePatientDto
{
    public HumanNameDto Name { get; init; } = null!;
    public Gender Gender { get; init; } = Gender.Unknown;
    public DateTime BirthDate { get; init; }
    public bool Active { get; init; } = true;
}