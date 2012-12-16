using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PersonAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "PersonGetById",
                routeTemplate: "myservice/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "PersonPost",
                routeTemplate: "myservice/{controller}/{action}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
