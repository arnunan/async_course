using Core.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Billing.Core.DB;

public class TotalMoneyEarnedDbContext : DbContext
{
    private readonly IDbSettings _settings;
    private readonly ILoggerFactory _loggerFactory;

    public TotalMoneyEarnedDbContext(
        IDbSettings settings,
        ILoggerFactory loggerFactory)
    {
        _settings = settings;
        _loggerFactory = loggerFactory;
    }

    public DbSet<TotalMoneyEarnedDbo> TotalMoneyEarneds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql(_settings.ConnectionString);
        builder.UseLoggerFactory(_loggerFactory);
    }

    public void Add(TotalMoneyEarnedDbo totalMoneyEarned)
    {
        TotalMoneyEarneds.Add(totalMoneyEarned);
        SaveChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var userDbo = modelBuilder.Entity<OperationsLogDbo>();
        userDbo.HasKey(x => x.Id);
    }
}