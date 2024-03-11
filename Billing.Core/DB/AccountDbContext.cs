using Core.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Billing.Core.DB;

public class AccountDbContext : DbContext
{
    private readonly IDbSettings _settings;
    private readonly ILoggerFactory _loggerFactory;

    public AccountDbContext(
        IDbSettings settings,
        ILoggerFactory loggerFactory)
    {
        _settings = settings;
        _loggerFactory = loggerFactory;
    }

    public DbSet<AccountDbo> Accounts { get; set; }

    public void Add(AccountDbo account)
    {
        Accounts.Add(account);
        SaveChanges();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql(_settings.ConnectionString);
        builder.UseLoggerFactory(_loggerFactory);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var templateDomainModelDbos = modelBuilder.Entity<OperationsLogDbo>();
        templateDomainModelDbos.HasKey(x => x.Id);
    }
}