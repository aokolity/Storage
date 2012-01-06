using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

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
        }
    }
}