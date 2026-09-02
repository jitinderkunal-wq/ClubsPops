using ClubPOS.API.Extensions;
using ClubPOS.API.Middleware;
using ClubPOS.Core.Entities.Identity;
using ClubPOS.Infrastructure.Data;
using ClubPOS.Infrastructure.Identity;
using ClubPOS.Services.JobScheduler;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using OTS.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Job Scheduler
builder.Services.AddHostedService<SchedulerService>();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(30); // 30 minutes
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseSwaggerDocumentation(app.Environment);

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Content")), RequestPath = "/Content"
});

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.MapFallbackToController("Index", "Fallback");

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var loggerFactory = services.GetRequiredService<ILoggerFactory>();

var context = services.GetRequiredService<DataContext>();
var identityContext = services.GetRequiredService<IdentityDbContext>();
var userManager = services.GetRequiredService<UserManager<AppUser>>();
var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
try
{
    await context.Database.MigrateAsync();
    await identityContext.Database.MigrateAsync();
    await SeedData.Seedasync(context, loggerFactory);
    await IdentityDbContextSeed.SeedUsersAndRolesAsync(userManager, roleManager);
}
catch (Exception ex)
{
    loggerFactory.CreateLogger<Program>().LogError(ex, "An error occured during migration");
}

app.Run();