using SalesOrder.Api.Models.Handlers;

namespace SalesOrder.Api.Models.Enrichers
{
    public class OrderItemEnricher : ResponseEnricher<OrderItemModel>
    {
        public OrderItemEnricher(IMediaLinkFactory mediaLinkFactory)
            : base(mediaLinkFactory)
        {
        }

        public override void Enrich(OrderItemModel content)
        {
            content.AddLink(CreateLink(MediaLinkType.Self, "OrderItem", new
            {
                salesOrderId = content.SalesOrderId, orderItemId = content.Id
            }));            
        }
    }
}