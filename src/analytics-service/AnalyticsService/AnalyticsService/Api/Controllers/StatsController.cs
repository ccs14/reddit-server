using AnalyticsService.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnalyticsService.Api.Controllers;

[ApiController]
[Route("stats")]
public sealed class StatsController : ControllerBase
{
    private readonly AnalyticsDbContext _db;

    public StatsController(AnalyticsDbContext db) => _db = db;

    [HttpGet("counts")]
    public async Task<IActionResult> GetCounts(CancellationToken ct)
    {
        var data = await _db.EventCounts
            .OrderByDescending(x => x.Count)
            .Select(x => new { x.EventType, x.Count })
            .ToListAsync(ct);

        return Ok(data);
    }
}