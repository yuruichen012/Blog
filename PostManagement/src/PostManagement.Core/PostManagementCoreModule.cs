using Autofac;
using PostManagement.Core.PostAggregates;
using AutofacModule = Autofac.Module;

namespace PostManagement.Core;

public class PostManagementCoreModule : AutofacModule
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<PostDomainService>().As<IPostDomainService>().InstancePerLifetimeScope();
    }
}
