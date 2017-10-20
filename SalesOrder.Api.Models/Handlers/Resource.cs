using System.Collections.Generic;
using Newtonsoft.Json;

namespace SalesOrder.Api.Models.Handlers
{
    public abstract class Resource
    {
        private readonly List<Link> _links = new List<Link>();

        [JsonProperty()]
        public IEnumerable<Link> Links => _links;

        public void AddLink(Link link)
        {
            _links.Add(link);
        }

        public void AddLinks(params Link[] links)
        {
            _links.AddRange(links);
        }
    }
}