using CEPDI.Repositories;
using CEPDI.Repositories.Interfaces;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace CEPDI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            
            container.Register<IFormasFarmaceuticasRepository, FormasFarmaceuticasRepository>(Lifestyle.Scoped);
            container.Register<IMedicamentosRepository, MedicamentosRepository>(Lifestyle.Scoped);
            container.Register<IUsuariosRepository, UsuariosRepository>(Lifestyle.Scoped);
            
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            // Here your usual Web API configuration stuff.

        }
        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }
        private bool IsWebApiRequest()
        {
            return (HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath
                        .StartsWith("api") ||
                        HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath
                        .StartsWith("~/api"));
        }
    }
}
