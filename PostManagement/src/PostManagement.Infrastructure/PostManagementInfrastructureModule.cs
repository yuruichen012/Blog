using System.Reflection;
using Ardalis.SharedKernel;
using Autofac;
using MediatR;
using MediatR.Pipeline;
using PostManagement.Core;
using PostManagement.Infrastructure.EntityFrameworkCore;
using PostManagement.Infrastructure.EntityFrameworkCore.Initializers;
using PostManagement.UseCases;
using Shared.EntityFrameworkCore;
using Module = Autofac.Module;

namespace PostManagement.Infrastructure;

/// <summary>
/// An Autofac module responsible for wiring up services defined in Infrastructure.
/// Mainly responsible for setting up EF and MediatR, as well as other one-off services.
/// </summary>
public class PostManagementInfrastructureModule : Module
{
    private readonly bool _isDevelopment = false;
    private readonly List<Assembly> _assemblies = [];

    public PostManagementInfrastructureModule(bool isDevelopment, Assembly? callingAssembly = null)
    {
        _isDevelopment = isDevelopment;
        AddToAssembliesIfNotNull(callingAssembly);
    }

    private void AddToAssembliesIfNotNull(Assembly? assembly)
    {
        if (assembly != null)
        {
            _assemblies.Add(assembly);
        }
    }

    private void LoadAssemblies()
    {
        // TODO: Replace these types with any type in the appropriate assembly/project
        var coreAssembly = Assembly.GetAssembly(typeof(PostManagementCoreModule));
        var infrastructureAssembly = Assembly.GetAssembly(typeof(PostManagementInfrastructureModule));
        var useCasesAssembly = Assembly.GetAssembly(typeof(PostManagementUseCasesModule));

        AddToAssembliesIfNotNull(coreAssembly);
        AddToAssembliesIfNotNull(infrastructureAssembly);
        AddToAssembliesIfNotNull(useCasesAssembly);
    }

    protected override void Load(ContainerBuilder builder)
    {
        LoadAssemblies();

        if (_isDevelopment)
        {
            RegisterDevelopmentOnlyDependencies(builder);
        }
        else
        {
            RegisterProductionOnlyDependencies(builder);
        }

        RegisterEF(builder);
        RegisterQueries(builder);
        RegisterMediatR(builder);
    }

    private void RegisterEF(ContainerBuilder builder)
    {
        // builder.RegisterType<PostManagementDbContext>().InstancePerLifetimeScope();

        builder.RegisterType<PostManagementDbContextInitializer>()
            .As<IDbContextInitializer>()
            .InstancePerLifetimeScope();

        builder.RegisterGeneric(typeof(EfCoreRepository<>))
            .As(typeof(IRepository<>))
            .As(typeof(IReadRepository<>))
            .InstancePerLifetimeScope();
    }

    private void RegisterQueries(ContainerBuilder builder)
    {
    }

    private void RegisterMediatR(ContainerBuilder builder)
    {
        builder
          .RegisterType<Mediator>()
          .As<IMediator>()
          .InstancePerLifetimeScope();

        builder
          .RegisterGeneric(typeof(LoggingBehavior<,>))
          .As(typeof(IPipelineBehavior<,>))
          .InstancePerLifetimeScope();

        builder
          .RegisterType<MediatRDomainEventDispatcher>()
          .As<IDomainEventDispatcher>()
          .InstancePerLifetimeScope();

        var mediatrOpenTypes = new[]
        {
            typeof(IRequestHandler<,>),
            typeof(IRequestExceptionHandler<,,>),
            typeof(IRequestExceptionAction<,>),
            typeof(INotificationHandler<>),
        };

        var array = _assemblies.ToArray();
        foreach (var mediatrOpenType in mediatrOpenTypes)
        {
            builder.RegisterAssemblyTypes(array)
                .AsClosedTypesOf(mediatrOpenType)
                .AsImplementedInterfaces();
        }
    }

    private void RegisterDevelopmentOnlyDependencies(ContainerBuilder builder)
    {
    }

    private void RegisterProductionOnlyDependencies(ContainerBuilder builder)
    {
    }
}
