namespace CleanArchitecture.Actors;

public static class ActorSerializer
{
    private const char Delimiter = '⏤';

    public static string Serialize(Actor? actor)
    {
        var values = Properties(actor);
        return string.Join(Delimiter, values.Select(Wrap));
    }

    private static object[] Properties(Actor? actor)
    {
        switch (actor)
        {
            case BrokerActor broker:
                return
                [
                    broker.Role,
                    broker.BrokerId,
                    broker.Username,
                    broker.DisplayName
                ];

            case CustomerActor customer:
                return
                [
                    customer.Role,
                    customer.CustomerId,
                    customer.Username,
                    customer.DisplayName
                ];

            case InternalServiceActor:
                return
                [
                    actor.Role,
                    actor.Username,
                    actor.DisplayName
                ];

            case ExternalServiceActor:
                return
                [
                    actor.Role,
                    actor.Username,
                    actor.DisplayName
                ];

            case not null:
                return
                [
                    actor.Role,
                    actor.Username,
                    actor.DisplayName
                ];

            default:
                return [Strings.Question];
        }

    }

    private static string Wrap(object value)
    {
        return $"[{value}]";
    }
}
