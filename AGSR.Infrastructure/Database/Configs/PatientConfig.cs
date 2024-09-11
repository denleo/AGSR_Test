using AGSR.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AGSR.Infrastructure.Database.Configs;

internal class PatientConfig : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patients");

        builder.HasKey(x => x.Id);
        builder.OwnsOne(x => x.Name, b =>
        {
            b.Property(x => x.Use).HasColumnName("NameUse");
            b.Property(x => x.Family).HasColumnName("FamilyName");
            b.Property(x => x.Given).HasColumnName("GivenNames");
        });
    }
}