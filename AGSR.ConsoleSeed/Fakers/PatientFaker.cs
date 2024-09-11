using AGSR.ConsoleSeed.Dtos;
using Bogus;

namespace AGSR.ConsoleSeed.Fakers;

internal sealed class PatientFaker : Faker<CreatePatientDto>
{
    public PatientFaker()
    {
        RuleFor(x => x.Name, _ => new HumanNameFaker().Generate());
        RuleFor(x => x.Gender, f => f.PickRandom<Gender>());
        RuleFor(x => x.BirthDate, f => f.Date.Between(DateTime.UtcNow.AddDays(-50), DateTime.UtcNow.AddDays(50)));
        RuleFor(x => x.Active, f => f.Random.Float() < 0.7F);
    }
}