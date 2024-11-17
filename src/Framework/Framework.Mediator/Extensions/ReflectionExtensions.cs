using System.Runtime.CompilerServices;

namespace Framework.Mediator.Extensions;

internal static class ReflectionExtensions
{
    public static bool IsNonAbstractClass(this Type type, bool publicOnly)
    {
        if (type.IsSpecialName)
        {
            return false;
        }

        if (type.IsClass && !type.IsAbstract)
        {
            if (type.HasAttribute<CompilerGeneratedAttribute>())
            {
                return false;
            }

            if (publicOnly)
            {
                return type.IsPublic || type.IsNestedPublic;
            }

            return true;
        }

        return false;
    }

    public static bool IsBasedOn(this Type type, Type otherType)
    {
        if (otherType.IsGenericTypeDefinition)
        {
            return type.IsAssignableToGenericTypeDefinition(otherType);
        }

        return otherType.IsAssignableFrom(type);
    }

    public static bool IsAssignableToGenericTypeDefinition(this Type type, Type genericType)
    {
        foreach (var interfaceType in type.GetInterfaces())
        {
            if (interfaceType.IsGenericType)
            {
                var genericTypeDefinition = interfaceType.GetGenericTypeDefinition();
                if (genericTypeDefinition == genericType)
                {
                    return true;
                }
            }
        }

        if (type.IsGenericType)
        {
            var genericTypeDefinition = type.GetGenericTypeDefinition();
            if (genericTypeDefinition == genericType)
            {
                return true;
            }
        }

        var baseType = type.BaseType;
        if (baseType is null)
        {
            return false;
        }

        return baseType.IsAssignableToGenericTypeDefinition(genericType);
    }

    private static bool HasAttribute<T>(this Type type) where T : Attribute
    {
        return type.IsDefined(typeof(T), inherit: true);
    }
}
