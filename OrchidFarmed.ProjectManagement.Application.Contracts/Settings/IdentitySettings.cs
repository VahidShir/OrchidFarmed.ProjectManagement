namespace OrchidFarmed.ProjectManagement.Application.Contracts.Settings;

public record IdentitySettings
{
    public string SecretKey { get; set; }
    public string ValidAudience { get; set; }
    public string ValidIssuer { get; set; }
}