using Bogus;

namespace Infrastructure.CommoditySystem.Mock.Fakers;

internal class CommodityFaker : Faker<Commodity>
{
    public CommodityFaker()
    {
        RuleFor(x => x.CommodityId, x => x.UniqueIndex);
        RuleFor(x => x.CommodityName, x => x.Commerce.Product());
    }
}
