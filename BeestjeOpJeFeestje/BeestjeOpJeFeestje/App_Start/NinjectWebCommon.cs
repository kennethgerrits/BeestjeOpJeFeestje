using System.Diagnostics.CodeAnalysis;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(BeestjeOpJeFeestje.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(BeestjeOpJeFeestje.App_Start.NinjectWebCommon), "Stop")]

namespace BeestjeOpJeFeestje.App_Start
{
    using System;
    using System.Web;
    using BeestjeOpJeFeestje.Domain;
    using BeestjeOpJeFeestje.Domain.Interface_Repositories;
    using BeestjeOpJeFeestje.Domain.Repositories;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject.Web.Common.WebHost;
    using Ninject;
    using Ninject.Web.Common;

    [ExcludeFromCodeCoverage]
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                kernel.Bind<BeesteOpJeFeestjeEntities>().ToSelf().InSingletonScope();
                kernel.Bind<IBeastRepository>().To<BeastRepository>().InSingletonScope();
                kernel.Bind<IAccessoryRepository>().To<AccessoryRepository>().InSingletonScope();
                kernel.Bind<IBoekingRepository>().To<BoekingRepository>().InSingletonScope();
                kernel.Bind<IContactpersonRepository>().To<ContactpersonRepository>().InSingletonScope();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
        }        
    }
}
