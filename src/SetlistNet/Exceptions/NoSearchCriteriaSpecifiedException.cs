using System;

namespace SetlistNet.Exceptions;

public class NoSearchCriteriaSpecifiedException(string? message = null) : Exception(message ?? DefaultMessage)
{
    public const string DefaultMessage = "Not Found: no search criteria specified";
    
    internal static void ThrowIfNullOrWhiteSpace(string arg)
    {
        if (string.IsNullOrWhiteSpace(arg))
        {
            throw new NoSearchCriteriaSpecifiedException(DefaultMessage);
        }
    }
}