using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Template.FrontApi.Db;
using Template.FrontApi.Db.Dbos;
using Template.FrontApi.Models;
using Template.FrontApi.Models.TemplateDomain;

namespace Template.FrontApi.Controllers;

public class DomainModelController : Controller
{
    private readonly TemplateApiDbContext _templateApiDbContext;

    public DomainModelController(Core.Db.ContextSupport.IDbContextFactory<TemplateApiDbContext> contextFactory)
    {
        _templateApiDbContext = contextFactory.CreateDbContext();
    }

    // Показываем страницу со списком
    public async Task<IActionResult> Index()
    {
        var result = await _templateApiDbContext.TemplateDomainModelDbos.ToListAsync();
        var mappedResult = result.Select(dbo => new TemplateDomainModel
        {
            Id = dbo.Id,
            Name = dbo.Name,
            Surname = dbo.Surname
        }).ToArray();
        return View(mappedResult);
    }
    
    
    // Показываем страницу с добавлением
    public async Task<IActionResult> Add()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Save(string name, string surname)
    {
        var result = await _templateApiDbContext.AddAsync(new TemplateDomainModelDbo
        {
            Id = Guid.NewGuid(),
            Name = name,
            Surname = surname
        });

        await _templateApiDbContext.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}