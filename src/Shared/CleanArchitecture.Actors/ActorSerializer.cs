namespace CleanArchitecture.Actors;

public static class ActorSerializer
{
    private const char Delimiter = '⏤';

    public static string Serialize(Actor? actor)
    {
        switch (actor)
        {
            case BrokerActor broker:
                {
                    var values = new object[]
                    {
                        broker.Role,
                        broker.BrokerId,
                        broker.Username,
                        broker.DisplayName
                    };

                    return string.Join(Delimiter, values.Select(x => Wrap(x)));
                }

            case CustomerActor customer:
                {
                    var values = new object[]
                    {
                        customer.Role,
                        customer.CustomerId,
                        customer.Username,
                        customer.DisplayName
                    };

                    return string.Join(Delimiter, values.Select(x => Wrap(x)));
                }

            case InternalServiceActor:
                {
                    var values = new object[]
                    {
                        actor.Role,
                        actor.Username,
                        actor.DisplayName
                    };

                    return string.Join(Delimiter, values.Select(x => Wrap(x)));
                }

            case ExternalServiceActor:
                {
                    var values = new object[]
                    {
                        actor.Role,
                        actor.Username,
                        actor.DisplayName
                    };

                    return string.Join(Delimiter, values.Select(x => Wrap(x)));
                }

            case not null:
                {
                    var values = new object[]
                    {
                        actor.Role,
                        actor.Username,
                        actor.DisplayName
                    };

                    return string.Join(Delimiter, values.Select(x => Wrap(x)));
                }

            default:
                return Strings.Question;
        }

    }

    public static Actor? Deserialize(string actor)
    {
        var array = actor.Split(Delimiter, System.StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = Unwrap(array[i]);
        }

        if (array.Length < 3)
        {
            return null;
        }

        if (!Enum.TryParse<Role>(array[0], out var role))
        {
            return null;
        }

        switch (role)
        {
            case Role.Broker:
                if (array.Length >= 4 && int.TryParse(array[1], out var brokerId))
                {
                    return new BrokerActor(brokerId, array[2], array[3], null);
                }
                break;

            case Role.Customer:
                if (array.Length >= 4 && int.TryParse(array[1], out var auctioneerId))
                {
                    return new CustomerActor(auctioneerId, array[2], array[3]);
                }
                break;

            case Role.Supervisor:
                return new SupervisorActor(array[1], array[2]);

            case Role.Administrator:
                return new Administrator(array[1], array[2]);

            case Role.Programmer:
                return new Programmer(array[1], array[2]);

            case Role.InternalService:
                return new InternalServiceActor(array[1], array[2]);

            case Role.ExternalService:
                return new ExternalServiceActor(array[1], array[2]);
        }

        return null;
    }

    private static string Wrap(object value)
    {
        return $"[{value}]";
    }

    private static string Unwrap(string value)
    {
        if (value.StartsWith('['))
        {
            value = value.Substring(1);
        }

        if (value.EndsWith(']'))
        {
            value = value.Substring(0, value.Length - 1);
        }

        return value;
    }
}
