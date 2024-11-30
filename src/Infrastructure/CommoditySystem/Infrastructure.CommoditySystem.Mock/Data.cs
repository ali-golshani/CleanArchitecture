namespace Infrastructure.CommoditySystem.Mock;

public static class Data
{
    public static readonly MockData<int> Customers = new MockData<int>
    (
        validValues: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10],
        invalidValues: [20, 21, 22]
    );

    public static readonly MockData<int> Commodities = new MockData<int>
    (
        validValues: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10],
        invalidValues: [20, 21, 22]
    );
}