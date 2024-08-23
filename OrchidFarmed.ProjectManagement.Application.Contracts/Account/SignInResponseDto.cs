namespace OrchidFarmed.ProjectManagement.Application.Contracts.Account;

public record SignInResponseDto
{
    public bool IsSuccessful { get; set; }
    public string Token { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string? Mobile { get; set; }
    public string ErrorMessage { get; set; }
}