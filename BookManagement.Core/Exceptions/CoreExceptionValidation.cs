namespace BookManagement.Core.Exceptions;
public class CoreExceptionValidation : Exception
{
    public CoreExceptionValidation(string message) : base(message)
    { }

    public static void When(bool hasError, string message)
    {
        if (hasError)
            throw new CoreExceptionValidation(message);
    }
}
