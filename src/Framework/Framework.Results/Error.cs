using Framework.Exceptions;
using System.Text;

namespace Framework.Results;

public class Error(ErrorType type, string message, params Fact[] facts)
{
    public ErrorType Type { get; } = type;
    public string Message { get; } = message;
    public Fact[] Facts { get; } = facts;

    public override string ToString()
    {
        var result = new StringBuilder().AppendLine($"{Type} Error : {Message}");

        if (Facts?.Length > 0)
        {
            foreach (var fact in Facts)
            {
                result.AppendLine(fact.ToString());
            }
        }

        return result.ToString();
    }
}
