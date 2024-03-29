﻿using Core.Db.Configuration;

namespace Template.FrontApi.Configuration;

public class TemplateApiApplicationSettings : IDbProperties
{
    public string ConnectionString { get; set; }
    
    public string AuthConnectionString { get; set; }
    
    public string RoleConnectionString { get; set; }

    public string User { get; set; }

    public string Password { get; set; }
}