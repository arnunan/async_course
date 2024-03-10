using Core.Db.Configuration;

namespace Billing.AccrualMoney.Consumer.Settings;

public class AppSettings : IDbProperties
{
    public string? Secret { get; set; }
    
    public string ConnectionString { get; set; }

    public string User { get; set; }
    
    public string Password { get; set; }
}