using System;

namespace SetlistNet.Exceptions;

public class NoSearchCriteriaSpecifiedException(string message) : Exception(message)
{
    internal static void ThrowIfNullOrWhiteSpace(string arg)
    {
        if (string.IsNullOrWhiteSpace(arg))
        {
            throw new NoSearchCriteriaSpecifiedException("Not Found: no search criteria specified");
        }
    }
}