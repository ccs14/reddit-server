namespace AnalyticsService.Infrastructure.Messaging;

public sealed class RabbitMqOptions
{
    public string HostName { get; init; } = default!;
    public int Port { get; init; }

    public string UserName { get; init; } = default!;
    public string Password { get; init; } = default!;

    public string VirtualHost { get; init; } = "/";
    public string QueueName { get; init; } = default!;
    public ushort PrefetchCount { get; init; } = 50;
}