using AnalyticsService.Infrastructure.Messaging;
using AnalyticsService.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace AnalyticsService.Api.Controllers;

[ApiController]
public sealed class HealthController : ControllerBase
{
    private readonly AnalyticsDbContext _db;
    private readonly RabbitMqOptions _mq;

    public HealthController(AnalyticsDbContext db, IOptions<RabbitMqOptions> mq)
    {
        _db = db;
        _mq = mq.Value;
    }

    [HttpGet("/health")]
    public IActionResult Health() => Ok(new { status = "ok" });

    [HttpGet("/ready")]
    public async Task<IActionResult> Ready(CancellationToken ct)
    {
        var postgresOk = await _db.Database.CanConnectAsync(ct);
        var rabbitOk = await CanConnectRabbitAsync(ct);

        if (postgresOk && rabbitOk) return Ok(new { status = "ready" });

        return StatusCode(503, new { status = "not_ready", postgresOk, rabbitOk });
    }

    private async Task<bool> CanConnectRabbitAsync(CancellationToken ct)
    {
        try
        {
            var factory = new ConnectionFactory
            {
                HostName = _mq.HostName,
                Port = _mq.Port,
                UserName = _mq.UserName,
                Password = _mq.Password,
                VirtualHost = _mq.VirtualHost
            };

            await using var conn = await factory.CreateConnectionAsync(ct);
            await using var channel = await conn.CreateChannelAsync(cancellationToken: ct);
            return true;
        }
        catch
        {
            return false;
        }
    }
}