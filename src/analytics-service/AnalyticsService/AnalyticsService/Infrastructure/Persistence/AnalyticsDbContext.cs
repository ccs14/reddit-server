using AnalyticsService.Domain.Stats;
using Microsoft.EntityFrameworkCore;

namespace AnalyticsService.Infrastructure.Persistence;

public sealed class AnalyticsDbContext : DbContext
{
    public AnalyticsDbContext(DbContextOptions<AnalyticsDbContext> options) : base(options)
    {
    }

    public DbSet<EventCount> EventCounts => Set<EventCount>();
}