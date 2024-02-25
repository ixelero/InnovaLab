using System.Reflection;

namespace InnovaLab.CountryExplorer.BusinessLogic.CustomExceptions;

public class NotFoundException : Exception
{
    public NotFoundException(MemberInfo type, dynamic entityId)
        : base($"{type.Name} with id {entityId} does not exist")
    {
    }

    public NotFoundException(MemberInfo type, string text)
        : base($"{type.Name} with {text} does not exist")
    {
    }

    public NotFoundException()
    {
    }

    public NotFoundException(string message)
        : base(message)
    {
    }

    public NotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
