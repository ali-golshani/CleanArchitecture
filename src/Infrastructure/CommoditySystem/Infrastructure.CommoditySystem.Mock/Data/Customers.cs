using Bogus;
using Framework.Test;

namespace Infrastructure.CommoditySystem.Mock.Data;

public static class Customers
{
    private static readonly int[] validIds = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
    private static readonly int[] invalidIds = [20, 21, 22];

    public static int ValidValue()
    {
        return validIds.PickRandom();
    }

    public static int InvalidValue()
    {
        return invalidIds.PickRandom();
    }

    internal static bool IsValid(int customerId)
    {
        return validIds.Contains(customerId);
    }
}