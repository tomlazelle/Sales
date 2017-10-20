using SalesOrder.Api.Models.Handlers;

namespace SalesOrder.Api.Models.Enrichers
{
    public class SalesOrderCreatedEnricher:ResponseEnricher<SalesOrderCreatedModel>
    {
        public SalesOrderCreatedEnricher(IMediaLinkFactory mediaLinkFactory)
            : base(mediaLinkFactory)
        {
        }

        public override void Enrich(SalesOrderCreatedModel content)
        {
            content.AddLink(CreateLink(MediaLinkType.Self, "SalesOrder", new { id = content.Id }));
        }
    }
}