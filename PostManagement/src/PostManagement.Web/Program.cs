using System.Globalization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using PostManagement.Core;
using PostManagement.Infrastructure;
using PostManagement.Infrastructure.EntityFrameworkCore;
using PostManagement.UseCases;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(b => 
{
    b.RegisterModule<PostManagementCoreModule>();
    b.RegisterModule<PostManagementUseCasesModule>();
    b.RegisterModule<PostManagementInfrastructureModule>();
}));

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
        theme: AnsiConsoleTheme.Code,
        formatProvider: CultureInfo.InvariantCulture)
    .WriteTo.File(
        path: "Logs/log.txt",
        rollingInterval: RollingInterval.Day)
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
    .Enrich.FromLogContext());

builder.Services.AddAuthorization();
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(o =>
{
    o.ShortSchemaNames = true;
});

builder.Services.AddDbContext<PostManagementDbContext>(options => 
{
    var connectionString = builder.Configuration.GetConnectionString("PostManagement");
    options.UseSqlServer(connectionString);
    options.UseLoggerFactory(NullLoggerFactory.Instance);
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapFastEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerGen();

    app.Services.GetRequiredService<DatabaseInitializer>().Initialize();
}

app.Run();
