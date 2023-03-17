using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class SampleEntityTypeConfiguration : IEntityTypeConfiguration<Sample>
{
    public void Configure(EntityTypeBuilder<Sample> sampleConfiguration)
    {
        sampleConfiguration.ToTable("Options", AppDbContext.DEFAULT_SCHEMA);

        sampleConfiguration.HasKey(option => option.Id);

        sampleConfiguration.Property(option => option.Name)
            .IsRequired();
    }
}