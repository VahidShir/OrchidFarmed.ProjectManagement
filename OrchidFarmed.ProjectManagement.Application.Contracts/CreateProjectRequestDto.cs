using System.ComponentModel.DataAnnotations;

namespace OrchidFarmed.ProjectManagement.Application.Contracts;

public record CreateProjectRequestDto
{
    [Required]
    [MaxLength(200)]
    public string? Name { get; set; }

    [Required]
    [MaxLength(500)]
    public string Description { get; set; }
}