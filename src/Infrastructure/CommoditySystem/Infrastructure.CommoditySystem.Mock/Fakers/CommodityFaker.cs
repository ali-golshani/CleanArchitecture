using Bogus;
using Infrastructure.CommoditySystem.Models;

namespace Infrastructure.CommoditySystem.Mock.Fakers;

internal class CommodityFaker : Faker<Commodity>
{
    public CommodityFaker()
    {
        CustomInstantiator(x =>
        {
            var id = x.IndexGlobal;
            var name = x.Commerce.Product();
            return new Commodity(id, name);
        });
    }
}
