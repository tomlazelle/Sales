using AutoMapper;
using SalesOrder.Domain.Aggregates;
using SalesOrder.Domain.Events;
using SalesOrder.Domain.Messages;

namespace SalesOrder.Domain.Configuration
{
    public class SalesOrderHandlerMapProfile : Profile
    {
        public SalesOrderHandlerMapProfile()
        {
            
                    CreateMap<CreateSalesOrderMessage, SalesOrderCreatedEvent>()
                        .ForCtorParam("id", x => x.MapFrom(sm => sm.Id))
                        .ForCtorParam("accountId", x => x.MapFrom(sm => sm.AccountId))
                        .ForCtorParam("shippingAddress", x => x.MapFrom(sm => sm.ShippingAddress))
                        .ForCtorParam("billingAddress", x => x.MapFrom(sm => sm.BillingAddress))
                        .ForCtorParam("paymentData", x => x.MapFrom(sm => sm.Payment))
                        .ForCtorParam("orderDate", x => x.MapFrom(sm => sm.OrderDate))
                        .ForCtorParam("subTotal", x => x.MapFrom(sm => sm.SubTotal))
                        .ForCtorParam("tax", x => x.MapFrom(sm => sm.Tax))
                        .ForCtorParam("total", x => x.MapFrom(sm => sm.Total))
                        .ForCtorParam("dollarsOff", x => x.MapFrom(sm => sm.DollarsOff))
                        .ForCtorParam("discountPercent", x => x.MapFrom(sm => sm.DiscountPercent))
                        .ForCtorParam("status", x => x.MapFrom(sm => sm.Status))
                        .ForCtorParam("orderType", x => x.MapFrom(sm => sm.OrderType))
                        .ForCtorParam("refNo", x => x.MapFrom(sm => sm.RefNo))
                        .ForCtorParam("items", x => x.MapFrom(sm => sm.Items))
                        ;

                    CreateMap<AddressMessage, Address>();
                    CreateMap<PersonMessage, Person>();
                    CreateMap<PaymentMessage, Payment>();
                    CreateMap<CreateSalesOrderItemMessage, CreateOrderItemEvent>();
                
        }
    }
}