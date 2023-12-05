using Autofac;
using PostManagement.Infrastructure.EntityFrameworkCore;
using SharedKernel;
using AutofacModule = Autofac.Module;

namespace PostManagement.Infrastructure;

public class PostManagementInfrastructureModule : AutofacModule
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IRepository<,>)).InstancePerLifetimeScope();
    }
}
