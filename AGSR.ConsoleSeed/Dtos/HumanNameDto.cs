using System.ComponentModel.DataAnnotations;

namespace AGSR.ConsoleSeed.Dtos;

public record HumanNameDto
{
    public Use Use { get; init; }
    [Required] public string Family { get; init; } = null!;
    public List<string> Given { get; init; } = new();
}