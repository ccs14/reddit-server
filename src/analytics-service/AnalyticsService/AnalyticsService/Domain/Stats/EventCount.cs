namespace AnalyticsService.Domain.Stats;

public sealed class EventCount
{
    public string EventType { get; private set; } = default!;
    public long Count { get; private set; }

    private EventCount() { }

    public EventCount(string eventType, long count)
    {
        EventType = eventType;
        Count = count;
    }

    public void Increment(long by = 1)
    {
        Count += by;
    }
}