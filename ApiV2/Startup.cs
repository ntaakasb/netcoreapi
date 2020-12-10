using ApiV2.ServiceProvider.Modules;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace ApiV2
{
    // ASP.NET Core docs for Autofac are here:
    // https://autofac.readthedocs.io/en/latest/integration/aspnetcore.html
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(builder => builder.MapControllers());
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Add any Autofac modules or registrations.
            // This is called AFTER ConfigureServices so things you
            // register here OVERRIDE things registered in ConfigureServices.
            //
            // You must have the call to `UseServiceProviderFactory(new AutofacServiceProviderFactory())`
            // when building the host or this won't be called.
            builder.RegisterModule(new DataModule());
            builder.RegisterModule(new ModelModule());
            builder.RegisterModule(new ServiceModule());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Use extensions from libraries to register services in the
            // collection. These will be automatically added to the
            // Autofac container.
            services.AddHttpContextAccessor();
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                // Use the default property (Pascal) casing
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
        }
    }
}