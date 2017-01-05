using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Routing;
using Sales.Api.Models.Handlers;
using StructureMap;

namespace Sales.Api.Handlers
{
    public class EnrichingHandler : DelegatingHandler
    {
        private readonly IContainer _container;

        public EnrichingHandler(IContainer container)
        {
            _container = container;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _container.Inject(new UrlHelper(request));

            return base.SendAsync(request, cancellationToken)
                .ContinueWith(task =>
                {
                    var response = task.Result;

                    var enrich = GetEnricher(response);

                    if (enrich != null && enrich.CanEnrich(response))
                    {
                        enrich.Enrich(response);
                    }

                    return response;
                });
        }

        private IResponseEnricher GetEnricher(HttpResponseMessage response)
        {
            if (!(response.Content is ObjectContent)) return null;

            var responseType = ((ObjectContent) response.Content).ObjectType;

            return TryGetEnricher(responseType);
        }

        private IResponseEnricher TryGetEnricher(Type responseType)
        {
            try
            {
                var handlerType = typeof(ResponseEnricher<>);
                var genericType = handlerType.MakeGenericType(responseType);

                return (IResponseEnricher)_container.GetInstance(genericType);

            }
            catch
            {
                return null;
            }
        }
    }
}