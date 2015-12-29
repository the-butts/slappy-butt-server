using Microsoft.Owin;
using SlappyButt.Api;

[assembly: OwinStartup(typeof(Startup))]

namespace SlappyButt.Api
{
    using System.Reflection;
    using System.Web.Http;
    using Ninject.Web.Common.OwinHost;
    using Ninject.Web.WebApi.OwinHost;
    using Owin;
    using SlappyButt.Common.Constants;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            AutoMapperConfig.RegisterMappings(Assembly.Load(Assemblies.WebApi));

            this.ConfigureAuth(app);

            var httpConfig = new HttpConfiguration();

            WebApiConfig.Register(httpConfig);

            httpConfig.EnsureInitialized();

            app
                .UseNinjectMiddleware(NinjectConfig.CreateKernel)
            .UseNinjectWebApi(httpConfig);
        }
    }
}
