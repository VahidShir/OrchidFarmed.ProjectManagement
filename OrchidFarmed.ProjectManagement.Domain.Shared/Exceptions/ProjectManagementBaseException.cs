namespace OrchidFarmed.ProjectManagement.Domain.Shared.Exceptions;

public class ProjectManagementBaseException : Exception
{
    public ProjectManagementBaseException(): base(){}
    public ProjectManagementBaseException(string message): base(message){}
    public ProjectManagementBaseException(string message, Exception innerException): base(message, innerException){}
}