namespace AGSR.Domain.Exceptions;

public class PatientNotFoundException : Exception
{
    public PatientNotFoundException(string message, Exception inner) : base(message, inner)
    {
    }

    public PatientNotFoundException(string message) : base(message)
    {
    }
}