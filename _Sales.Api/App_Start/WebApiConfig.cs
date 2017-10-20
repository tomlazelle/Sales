using System.Web.Http;
using Sales.Api.Handlers;

namespace Sales.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            var handler = (EnrichingHandler) GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(EnrichingHandler));
            config.MessageHandlers.Add(handler);
            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}