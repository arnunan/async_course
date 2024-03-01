using Core.Db.Configuration;

namespace AuthService.Settings;

public class AppSettings : IDbProperties
{
    public string? Secret { get; set; }
    
    public string ConnectionString { get; set; }
    
    public string AuthConnectionString { get; set; }
    
    public string RoleConnectionString { get; set; }

    public string User { get; set; }
    
    public string Password { get; set; }
}