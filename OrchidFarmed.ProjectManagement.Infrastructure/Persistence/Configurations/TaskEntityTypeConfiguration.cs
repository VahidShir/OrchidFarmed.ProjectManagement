using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Task = OrchidFarmed.ProjectManagement.Domain.Task;

namespace OrchidFarmed.ProjectManagement.Infrastructure.Persistence.Configurations;

internal class TaskEntityTypeConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> modelBuilder)
    {
        modelBuilder
            .ToTable("Tasks")
            .HasKey(x => x.Id);

        modelBuilder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200); //design decision

        modelBuilder
            .Property(x => x.Description)
            .HasMaxLength(500) //design decision
            .IsRequired();

        modelBuilder
            .Property(x => x.DueDate)
            .IsRequired();
    }
}