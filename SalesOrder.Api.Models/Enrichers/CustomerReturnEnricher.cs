using SalesOrder.Api.Models.Handlers;

namespace SalesOrder.Api.Models.Enrichers
{
    public class CustomerReturnEnricher:ResponseEnricher<CustomerReturnModel>
    {
        public CustomerReturnEnricher(IMediaLinkFactory mediaLinkFactory)
            : base(mediaLinkFactory)
        {
        }

        public override void Enrich(CustomerReturnModel content)
        {
            content.AddLink(CreateLink(MediaLinkType.Self, "CustomerReturn", new
            {
                salesOrderId = content.SalesOrderId,
                returnId = content.ReturnId
            }));            
        }
    }
}