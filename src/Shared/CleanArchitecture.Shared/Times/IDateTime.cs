namespace CleanArchitecture.Shared;

public interface IDateTime
{
    DateTime Now { get; }
    DateOnly Today { get; }

    public DateOnly PreviousWeek => Today.AddDays(-7);
}
