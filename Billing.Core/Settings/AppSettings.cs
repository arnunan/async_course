using Core.Db.Configuration;

namespace Billing.Core.Settings;

public class AppSettings : IDbProperties
{
    public string ConnectionString { get; set; }

    public string User { get; set; }
    
    public string Password { get; set; }
}