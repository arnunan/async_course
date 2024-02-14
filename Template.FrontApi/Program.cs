using Core.Service.Domain.Startup;
using Template.FrontApi.Configuration;
using Template.FrontApi.Db.Configuration;
using Template.FrontApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add settings
builder.Services.AddAsyncCourseProperties();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DB settings
builder.Services.AddAsyncCourseDbSettings<TemplateApiApplicationSettings>();
builder.Services.AddAsyncCourseDomain();
builder.Services.AddAsyncCourseDbContext();


// app section
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=DomainModel}/{action=Index}/{id?}");

app.Run();