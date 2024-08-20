using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OrchidFarmed.ProjectManagement.Domain;

namespace OrchidFarmed.ProjectManagement.Infrastructure.Persistence.Configurations;

internal class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> modelBuilder)
    {
        modelBuilder
            .ToTable("Projects")
            .HasKey(x => x.Id);

        modelBuilder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder
            .Property(x => x.Descroption)
            .IsRequired()
            .HasMaxLength(500);

        modelBuilder
            .HasMany(x => x.Tasks)
            .WithOne()
            .HasForeignKey(x => x.ProjectId)
            .IsRequired();

        modelBuilder
            .Metadata
            .FindNavigation("Tasks")
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
