using Sales.Api.Models.Handlers;
using StructureMap;

namespace Sales.Api.Models.Configuration
{
    public class ModelRegistry : Registry
    {
        public ModelRegistry()
        {
            Scan(x =>
            {
                x.AssemblyContainingType(typeof(ModelRegistry));
                x.ConnectImplementationsToTypesClosing(typeof(ResponseEnricher<>));
            });

            //the media link factory uses UrlHelper -- this gets injected in the EnrichingHandler
            For<IMediaLinkFactory>().Use<MediaLinkFactory>();
        }
    }
}