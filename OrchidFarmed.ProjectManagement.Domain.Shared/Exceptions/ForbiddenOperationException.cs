namespace OrchidFarmed.ProjectManagement.Domain.Shared.Exceptions;

public class ForbiddenOperationException : BusinessException
{
    public ForbiddenOperationException() : base("The operation is forbidden.")
    {

    }

    public ForbiddenOperationException(string message) : base(message)
    {

    }
}