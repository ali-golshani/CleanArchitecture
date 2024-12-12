namespace Framework.Exceptions;

public class UnknownException : BaseSystemException
{
    public UnknownException(Exception innerException) : base(innerException)
    { }

    public override bool ShouldLog => true;
    public override bool IsFatal => true;
    public override string Message => "خطای نامشخص";
}
