namespace OrchidFarmed.ProjectManagement.Domain.Shared.Exceptions;

public class TaskNotFoundException: BusinessException
{
    public TaskNotFoundException(): base("The task entity not found.")
    {
        
    }
}