using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AspectsMvcApplication.DataAccess;
using AspectsMvcApplication.Interceptors;
using AspectsMvcApplication.Services;
using Autofac;
using Autofac.Extras.DynamicProxy2;
using Autofac.Integration.Mvc;

namespace AspectsMvcApplication
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private IContainer _container;

        protected void Application_Start()
        {
            SetUpContainer();

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void SetUpContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            RegisterControllers(builder);
            InjectHttpAbstractions(builder);
            RegisterCustomServices(builder);

            _container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));
        }

        private void RegisterControllers(ContainerBuilder builder)
        {
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
        }

        private void InjectHttpAbstractions(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacWebTypesModule());
        }

        private void RegisterCustomServices(ContainerBuilder builder)
        {
            builder.RegisterType<GamesDataAccess>().As<IGamesDataAccess>().SingleInstance();
            builder.RegisterType<LoggingService>().As<ILoggingService>();

            //builder.RegisterType<DirtySubscriptionService>().As<ISubscriptionService>();
            //builder.RegisterType<DirtyRedemptionService>().As<IRedemptionService>();

            builder.RegisterType<CleanSubscriptionService>()
                .As<ISubscriptionService>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(DefensiveProgrammingAspect))
                .InterceptedBy(typeof(LoggingAspect))
                .InterceptedBy(typeof(ExceptionsAspect))
                .InterceptedBy(typeof(TransactionAspect));

            builder.RegisterType<CleanRedemptionService>()
                .As<IRedemptionService>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(DefensiveProgrammingAspect))
                .InterceptedBy(typeof(LoggingAspect))
                .InterceptedBy(typeof(ExceptionsAspect))
                .InterceptedBy(typeof(TransactionAspect));

            builder.RegisterType<LoggingAspect>();
            builder.RegisterType<DefensiveProgrammingAspect>();
            builder.RegisterType<ExceptionsAspect>();
            builder.RegisterType<TransactionAspect>();
            builder.RegisterType<JsonValidatorAspect>();
            builder.RegisterType<ServiceAspect>();

            builder.RegisterType<ExternalServiceSimulator>().SingleInstance();
//            builder.RegisterType<DirtyServiceHandler>().As<IServiceHandler>();
//            builder.RegisterType<DirtyServiceParser>().As<IServiceParser>();

            builder.RegisterType<CleanJsonToGamePricesParser>()
                   .As<IJsonToGamePricesParser>()
                   .EnableInterfaceInterceptors()
                   .InterceptedBy(typeof (JsonValidatorAspect));

            builder.RegisterType<CleanExternalServiceHandler>()
                   .As<IExternalServiceHandler>()
                   .EnableInterfaceInterceptors()
                   .InterceptedBy(typeof(ServiceAspect));

            //builder.RegisterType<DirtyResourceService>().As<IResourceService>();
            builder.RegisterType<CleanResourceService>().As<IResourceService>();

            //builder.RegisterType<DirtyPaymentsReportingService>().As<IPaymentsReportingService>();
            builder.RegisterType<CleanPaymentsReportingService>().As<IPaymentsReportingService>();
        }
    }
}