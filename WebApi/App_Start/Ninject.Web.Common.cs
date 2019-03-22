[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebApi.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(WebApi.App_Start.NinjectWebCommon), "Stop")]

namespace WebApi.App_Start
{
    using System;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using Ninject;
    using Ninject.Modules;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Ninject.Web.WebApi;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using BLL.Infrastructure;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            NinjectModule serviceModule = new ServiceModule("DbConnection");
            var kernel = new StandardKernel(serviceModule);
            kernel.Unbind<ModelValidatorProvider>();
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
            return kernel;
        }

        private static void RegisterServices(IKernel kernel)
        {
        }
    }
}