﻿using Core.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Template.FrontApi.Db.Dbos;

namespace Template.FrontApi.Db;

public class TemplateApiDbContext : DbContext
{
    private readonly IDbSettings _settings;
    private readonly ILoggerFactory _loggerFactory;

    public TemplateApiDbContext(
        IDbSettings settings,
        ILoggerFactory loggerFactory)
    {
        _settings = settings;
        _loggerFactory = loggerFactory;
    }

    public DbSet<TemplateDomainModelDbo> TemplateDomainModelDbos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql(_settings.ConnectionString);
        builder.UseLoggerFactory(_loggerFactory);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var templateDomainModelDbos = modelBuilder.Entity<TemplateDomainModelDbo>();
        templateDomainModelDbos.HasKey(x => x.Id);
    }
}