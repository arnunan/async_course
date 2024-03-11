using Core.Db.ContextSupport;
using Microsoft.EntityFrameworkCore;

namespace Billing.Core.DB;

public class AuthDesignTimeDbContextFactory<T, T1> : DesignTimeDbContextFactoryBase<T, T1>
    where T : DbContext where T1 : IDbContextCreator<T>, new()
{
    protected override string MigrationDatabaseName { get; } = "Test";
}