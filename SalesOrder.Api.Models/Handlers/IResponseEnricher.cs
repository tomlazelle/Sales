using System.Net.Http;

namespace SalesOrder.Api.Models.Handlers
{
    public interface IResponseEnricher
    {
        bool CanEnrich(HttpResponseMessage response);
        HttpResponseMessage Enrich(HttpResponseMessage response);
    }
}