using System.Reflection;
using Autofac;
using MediatR;
using MediatR.Pipeline;
using PostManagement.Core;
using PostManagement.Infrastructure.EntityFrameworkCore;
using PostManagement.UseCases;
using SharedKernel;
using AutofacModule = Autofac.Module;

namespace PostManagement.Infrastructure;

public class PostManagementInfrastructureModule : AutofacModule
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterMediatR(builder);
        RegisterRepositories(builder);
    }

    private void RegisterRepositories(ContainerBuilder builder)
    {
        builder.RegisterType<DatabaseInitializer>();
        builder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IRepository<,>)).InstancePerLifetimeScope();
    }

    private void RegisterMediatR(ContainerBuilder builder)
    {
        builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();

        foreach (var assembly in (Assembly[])([
                typeof(PostManagementCoreModule).Assembly,
                typeof(PostManagementInfrastructureModule).Assembly,
                typeof(PostManagementUseCasesModule).Assembly ])
            )
        {
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IRequestHandler<,>)).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IRequestExceptionHandler<,,>)).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IRequestExceptionAction<,>)).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(INotificationHandler<>)).AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
