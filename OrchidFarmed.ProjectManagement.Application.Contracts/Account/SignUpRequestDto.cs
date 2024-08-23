using System.ComponentModel.DataAnnotations;

namespace OrchidFarmed.ProjectManagement.Application.Contracts.Account;

public record SignUpRequestDto
{
    [Required(ErrorMessage = "UserName is required")]
    [MaxLength(100)]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; }

    [RegularExpression("09(1[0-9]|3[1-9]|2[1-9])-?[0-9]{3}-?[0-9]{4}")]
    //Regular expression for Iran mobile phones
    //https://www.datisnetwork.com/phone-number-regex.html
    public string? Mobile { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MaxLength(500)]
    public string Password { get; set; }
}