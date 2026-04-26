namespace CleanArchitecture.IntegrationTests.Extensions;

internal static class ResultExtensions
{
    public static void AssertSuccess<T>(this Framework.Results.Result<T> result)
    {
        WriteErrors(result, MessageType.Error);
        Assert.IsTrue(result.IsSuccess);
    }

    public static void AssertFailure<T>(this Framework.Results.Result<T> result)
    {
        WriteErrors(result, MessageType.Info);
        Assert.IsTrue(result.IsFailure);
    }

    private static void WriteErrors<T>(this Framework.Results.Result<T> result, MessageType messageType)
    {
        var color = Console.ForegroundColor;
        Console.ForegroundColor = messageType == MessageType.Error ? ConsoleColor.Red : ConsoleColor.Green;

        var symbole = messageType == MessageType.Error ? "⛔" : "✅";

        if (result.IsFailure)
        {
            foreach (var item in result.Errors)
            {
                Console.WriteLine($"{symbole} {item.Message}");
            }
        }

        Console.ForegroundColor = color;
    }

    private enum MessageType
    {
        Info = 0,
        Error = 1,
    }
}
