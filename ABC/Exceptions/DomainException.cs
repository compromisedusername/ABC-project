namespace ABC.Exceptions;

public class DomainException : Exception
{
    public string Message { get; set; }
    public int StatusCode { get; set; }
}