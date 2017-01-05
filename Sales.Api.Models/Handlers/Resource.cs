using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sales.Api.Models.Handlers
{
    public abstract class Resource
    {
        private readonly List<Link> _links = new List<Link>();

        [JsonProperty(Order = 100)]
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