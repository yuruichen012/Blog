using Ardalis.ListStartupServices;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FastEndpoints;
using FastEndpoints.Swagger;
using PostManagement.Core;
using PostManagement.Infrastructure;
using PostManagement.Infrastructure.EntityFrameworkCore;
using PostManagement.UseCases;
using Serilog;
using Shared.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddPostManagementDbContext(builder.Configuration.GetConnectionString("DefaultConnection")!);

// builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(o =>
{
    o.ShortSchemaNames = true;
});

// add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
builder.Services.Configure<ServiceConfig>(config =>
{
    config.Services = new List<ServiceDescriptor>(builder.Services);

    // optional - default path to view services is /listallservices - recommended to choose your own path
    config.Path = "/listservices";
});


builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new PostManagementCoreModule());
    containerBuilder.RegisterModule(new PostManagementInfrastructureModule(builder.Environment.IsDevelopment()));
    containerBuilder.RegisterModule(new PostManagementUseCasesModule());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseShowAllServicesMiddleware(); // see https://github.com/ardalis/AspNetCoreStartupServices
}
else
{
    app.UseDefaultExceptionHandler(); // from FastEndpoints
    app.UseHsts();
}
// app.UseFastEndpoints();
// app.UseSwaggerGen(); // FastEndpoints middleware

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    foreach (var initializer in scope.ServiceProvider.GetServices<IDbContextInitializer>())
    {
        await initializer.InitializeAsync();
    }
}

app.Run();
