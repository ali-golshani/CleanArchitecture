namespace Infrastructure.CommoditySystem.Mock;

public class MockData<T>
{
    private static readonly Random random = new Random();

    private readonly T[] validValues;
    private readonly T[] invalidValues;

    public MockData(T[] validValues, T[] invalidValues)
    {
        this.validValues = validValues;
        this.invalidValues = invalidValues;
    }

    public T ValidValue()
    {
        return validValues[random.Next(validValues.Length)];
    }

    public T InvalidValue()
    {
        return invalidValues[random.Next(invalidValues.Length)];
    }

    public bool IsValid(T value)
    {
        return !invalidValues.Contains(value);
    }
}
