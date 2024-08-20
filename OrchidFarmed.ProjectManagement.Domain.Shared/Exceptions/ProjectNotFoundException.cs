namespace OrchidFarmed.ProjectManagement.Domain.Shared.Exceptions;

public class ProjectNotFoundException: BusinessException
{
    public ProjectNotFoundException(): base("The project entity not found.")
    {
        
    }
}