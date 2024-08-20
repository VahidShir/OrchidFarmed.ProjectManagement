

namespace OrchidFarmed.ProjectManagement.Domain.Shared.Exceptions;

public class BusinessException : ProjectManagementBaseException
{
    public BusinessException(){}

    public BusinessException(string message) : base(message){}

    public BusinessException(string message, Exception innerException) : base(message, innerException){}
}