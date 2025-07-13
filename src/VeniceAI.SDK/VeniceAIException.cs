using System;

namespace VeniceAI.SDK;

/// <summary>
/// Exception thrown by Venice AI SDK operations.
/// </summary>
public class VeniceAIException : Exception
{
    /// <summary>
    /// Initializes a new instance of the VeniceAIException class.
    /// </summary>
    /// <param name="message">The error message.</param>
    public VeniceAIException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the VeniceAIException class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public VeniceAIException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
