using Framework.Exceptions;
using System.Text;

namespace Framework.Results;

public class Error(string code, ErrorType type, string message, params Fact[] facts)
{
    public string Code { get; } = code;
    public ErrorType Type { get; } = type;
    public string Message { get; } = message;
    public Fact[] Facts { get; } = facts;

    public override string ToString()
    {
        var result = new StringBuilder().AppendLine($"{Code} ({Type}) Error : {Message}");

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
