using AGSR.ConsoleSeed.Dtos;
using Bogus;

namespace AGSR.ConsoleSeed.Fakers;

internal sealed class HumanNameFaker: Faker<HumanNameDto>
{
    public HumanNameFaker()
    {
        RuleFor(x => x.Use, f => f.PickRandom<Use>());
        RuleFor(x => x.Family, f => f.Name.LastName());
        RuleFor(x => x.Given, f => Enumerable.Range(1, f.Random.Int(0, 2)).Select(_ => f.Name.FirstName()).ToList());
    }
}