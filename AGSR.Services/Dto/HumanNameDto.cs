using System.ComponentModel.DataAnnotations;
using AGSR.Domain.Enums;

namespace AGSR.Services.Dto;

public record HumanNameDto
{
    public Use Use { get; init; }
    [Required] public string Family { get; init; } = null!;
    public List<string> Given { get; init; } = new();
}