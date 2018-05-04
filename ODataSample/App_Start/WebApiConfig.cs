using System.Web.Http;
using System.Web.OData.Builder;
using ODataSample.Models;
using System.Web.OData.Extensions;
using ODataSample.Repositories.Models;

namespace ODataSample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            ODataModelBuilder builder = new ODataConventionModelBuilder();
            config.Count().Filter().OrderBy().Expand().Select().MaxTop(null);

            builder.EntitySet<Product>("Products");
            builder.EntitySet<Customer>("Customers");
            builder.EntitySet<Country>("Countries");

            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model: builder.GetEdmModel());
        }
    }
}
