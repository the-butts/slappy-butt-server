namespace SlappyButt.Api
{
    using System;
    using System.Data.Entity;
    using System.Web;
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Web.Common;
    using SlappyButt.Common.Constants;
    using SlappyButt.Data;
    using SlappyButt.Data.Contracts;

    public static class NinjectConfig
    {
        public static Action<IKernel> DependenciesRegistration = kernel =>
        {
            kernel
                .Bind<DbContext>()
                .To<SlappyButtDbContext>()
                .InRequestScope();

            kernel
                .Bind(typeof(IRepository<>))
                .To(typeof(EfGenericRepository<>));
        };

        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                // ObjectFactory.Initialize(kernel);
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            DependenciesRegistration(kernel);

            kernel
                .Bind(b => b.From(Assemblies.DataServices)
                    .SelectAllClasses()
                    .BindDefaultInterface());

            kernel
             .Bind(b => b.From(Assemblies.LogicServices)
                 .SelectAllClasses()
                 .BindDefaultInterface());
        }
    }
}
