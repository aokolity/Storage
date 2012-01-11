using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Storage.ValidatorProviders;

namespace Storage
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "InvoicePrint",
                "Invoice/Print/{id}",
                new { controller = "Invoice", action = "Print", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "InvoiceDelete",
                "Invoice/{action}/{type}/{id}",
                new { controller = "Invoice", action = "Delete" }
            ); 

            routes.MapRoute(
                "InvoiceIndex",
                "Invoice/{action}/{type}",
                new { controller = "Invoice", action = "Index" }
            ); 

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            var existingProvider = ModelValidatorProviders.Providers.Single(x => x is ClientDataTypeModelValidatorProvider);

            ModelValidatorProviders.Providers.Remove(existingProvider);
            ModelValidatorProviders.Providers.Add(new ClientNumberValidatorProvider());
        }
    }
}