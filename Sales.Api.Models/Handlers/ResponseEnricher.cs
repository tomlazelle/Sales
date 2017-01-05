using System;
using System.Net.Http;

namespace Sales.Api.Models.Handlers
{
    public abstract class ResponseEnricher<T> : IResponseEnricher
    {
        private readonly IMediaLinkFactory _mediaLinkFactory;

        protected ResponseEnricher(IMediaLinkFactory mediaLinkFactory)
        {
            _mediaLinkFactory = mediaLinkFactory;
        }

        public virtual bool CanEnrich(Type contentType)
        {
            return contentType == typeof(T);
        }

        public abstract void Enrich(T content);

        bool IResponseEnricher.CanEnrich(HttpResponseMessage response)
        {
            var content = response.Content as ObjectContent;
            return (content != null && CanEnrich(content.ObjectType));
        }

        HttpResponseMessage IResponseEnricher.Enrich(HttpResponseMessage response)
        {
            T content;
            if (response.TryGetContentValue(out content))
            {
                Enrich(content);
            }

            return response;
        }


        public Link CreateLink(MediaLinkType linkType, string controllerName, object values)
        {
            return _mediaLinkFactory.CreateLink(linkType, controllerName, values);
        }

        public Link CreateLink(string controllerName, object values, string method, string relation)
        {
            return _mediaLinkFactory.CreateLink(controllerName, values, method, relation);
        }
    }
}