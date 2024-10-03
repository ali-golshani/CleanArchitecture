using System.Text;

namespace Framework.Results;

public class Error(ErrorType type, string message, params ErrorSource[] sources)
{
    public ErrorType Type { get; } = type;
    public string Message { get; } = message;
    public ErrorSource[] Sources { get; } = sources;

    public override string ToString()
    {
        var result =
            new StringBuilder()
            .AppendLine($"{Type} Error")
            .AppendLine(Message)
            ;

        if (Sources?.Length > 0)
        {
            foreach (var item in Sources)
            {
                result.AppendLine(item.ToString());
            }
        }

        return result.ToString();
    }
}
