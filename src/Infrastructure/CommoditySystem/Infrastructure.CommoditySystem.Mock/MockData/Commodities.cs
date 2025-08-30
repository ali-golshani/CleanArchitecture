using Framework.Test;
using Infrastructure.CommoditySystem.Fakers;
using Infrastructure.CommoditySystem.Models;

namespace Infrastructure.CommoditySystem.MockData;

public static class Commodities
{
    private static readonly Commodity[] validValues;
    private static readonly Commodity[] invalidValues;

    static Commodities()
    {
        var faker = new CommodityFaker();

        validValues = [.. faker.Generate(100)];
        invalidValues = [.. faker.Generate(10)];
    }

    public static Commodity ValidValue()
    {
        return validValues.PickRandom();
    }

    public static Commodity InvalidValue()
    {
        return invalidValues.PickRandom();
    }

    internal static Commodity? Find(int commodityId)
    {
        return validValues.FirstOrDefault(x => x.CommodityId == commodityId);
    }

    internal static bool IsValid(int commodityId)
    {
        return validValues.Any(x => x.CommodityId == commodityId);
    }
}