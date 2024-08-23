using System.ComponentModel.DataAnnotations;

namespace OrchidFarmed.ProjectManagement.Application.Contracts.Account;

public record SignInRequestDto
{
    [Required(ErrorMessage = "UserName is required")]
    [MaxLength(100)]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MaxLength(length: 500)]
    public string Password { get; set; }
}