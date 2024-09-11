using AGSR.Domain.Enums;

namespace AGSR.Domain.ValueObjects;

public class HumanName
{
    public Use Use { get; init; }
    public string Family { get; init; } = null!;
    public List<string> Given { get; init; } = new();
}