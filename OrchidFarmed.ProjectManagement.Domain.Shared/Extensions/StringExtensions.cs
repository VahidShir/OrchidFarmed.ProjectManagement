namespace OrchidFarmed.ProjectManagement.Domain.Shared.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrEmptyWhiteSpace(this string value)
    {
        return string.IsNullOrWhiteSpace(value);
    }
}