using System.ComponentModel.DataAnnotations;

namespace OrchidFarmed.ProjectManagement.Application.Contracts;

public record CreateTaskRequestDto
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; }

    [Required]
    [MaxLength(500)]
    public string Description { get; set; }

    public DateTime DueDate { get; set; }
}