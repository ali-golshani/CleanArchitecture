using Framework.Test;
using Infrastructure.CommoditySystem.Mock.Fakers;

namespace Infrastructure.CommoditySystem.Mock.Data;

public static class Commodities
{
    private static readonly Commodity[] validValues;
    private static readonly Commodity[] invalidValues;

    static Commodities()
    {
        var faker = new CommodityFaker();

        validValues = faker.Generate(100).ToArray();
        invalidValues = faker.Generate(10).ToArray();
    }

    public static Commodity ValidValue()
    {
        return validValues.PickRandom();
    }

    public static Commodity InvalidValue()
    {
        return invalidValues.PickRandom();
    }

    public static bool IsValid(int commodityId)
    {
        return validValues.Any(x => x.CommodityId == commodityId);
    }
}