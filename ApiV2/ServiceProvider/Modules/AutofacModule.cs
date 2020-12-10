using Autofac;
using ApiV2.ServiceProvider.BaseFactory;
using ApiV2.ServiceProvider.BaseFactory.Interface;
using ServiceContext.Provider.SQL;
using ServiceContext.Provider.SQL.Interface;

namespace ApiV2.ServiceProvider.Modules
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RepositoryUnitOfWork>().As<IRepositoryUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<BaseDbContextFactory>().As<IBaseDbContextFactory>().InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<IBaseDbContextFactory>().GetInstance())
                   .As<RepositoryDbContext>().InstancePerLifetimeScope();
        }
    }
    public class ModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(System.Reflection.Assembly.Load("ModelContext"))
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope();
        }
    }
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(System.Reflection.Assembly.Load("ServiceContext"))
                      .AsImplementedInterfaces()
                      .InstancePerLifetimeScope();
        }
    }
}