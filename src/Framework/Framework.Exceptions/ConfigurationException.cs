namespace Framework.Exceptions;

public class ConfigurationException(string message) : BaseSystemException(message)
{
    public override bool IsFatal => true;
    public override bool ShouldLog => true;
}
