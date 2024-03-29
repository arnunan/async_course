﻿namespace Core.Db.Configuration;

public interface IDbProperties
{
    public string ConnectionString { get; set; }
    
    public string User { get; set; }
    
    public string Password { get; set; }
}