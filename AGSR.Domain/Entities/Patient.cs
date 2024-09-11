using AGSR.Domain.Enums;
using AGSR.Domain.ValueObjects;

namespace AGSR.Domain.Entities;

public class Patient : BaseEntity
{
    public HumanName Name { get; init; } = null!;
    public Gender Gender { get; init; } = Gender.Unknown;
    public DateTime BirthDate { get; init; }
    public bool Active { get; init; } = true;
}