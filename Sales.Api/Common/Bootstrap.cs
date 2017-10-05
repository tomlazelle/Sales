using System.Linq;
using AutoMapper;
using Sales.Api.Handlers;
using Sales.Api.Models;
using Sales.Common;
using Sales.Domain.Aggregates;
using Sales.Domain.Configuration;
using Sales.Domain.Handlers;
using StructureMap;

namespace Sales.Api.Common
{
    public static class Bootstrap
    {
        public static IContainer Init()
        {
            var container = IoC();

            SetupDatabase(container);

            return container;
        }

        public static void SetupDatabase(IContainer container)
        {
            container.Configure(x => x.AddRegistry<DomainRegistry>());
        }

        public static IMapper SetupAutoMapper()
        {
            return new MapperConfiguration(x =>
                {
                    x.AddProfiles(typeof(Bootstrap).Assembly);
                    x.AddProfiles(typeof(SalesOrderHandler));
                }
            ).CreateMapper();
        }

        public static IContainer IoC()
        {
            return new Container(x =>
            {
                x.ForConcreteType<EnrichingHandler>();
                x.For<IConfigMgr>().Use<ConfigMgr>();
                x.ForConcreteType<SalesOrderHandler>();
                x.ForConcreteType<SalesOrderQueryHandler>();

                x.For<IMapper>().Use(SetupAutoMapper());
                x.Scan(
                    scan =>
                    {
                        scan.TheCallingAssembly();
                        scan.WithDefaultConventions();
                    });
            });
        }
    }

    public class SalesOrderMapperProfile : Profile
    {
        public SalesOrderMapperProfile()
        {
            CreateMap<SalesOrder, SalesOrderModel>()
                .ForMember(dm => dm.ShippingAddress, mo => mo.MapFrom(sm => sm.ShippingAddress))
                .ForMember(dm => dm.BillingAddress, mo => mo.MapFrom(sm => sm.BillingAddress))
                .ForMember(dm => dm.Customer, mo => mo.MapFrom(sm => sm.Customer))
                .ForMember(dm => dm.Returns, mo => mo.MapFrom(sm =>
                from child in sm.Returns
                select new CustomerReturnModel
                {
                    SalesOrderId = sm.Id,
                    Quantity = child.Quantity,
                    Id = child.Id,
                    Action = child.Action,
                    Amount = child.Amount,
                    Notes = child.Notes,
                    Reason = child.Reason,
                    ReturnDate = child.ReturnDate,
                    ReturnId = child.ReturnId,
                    Sku = child.Sku,
                    Status = child.Status
                }))
                .ForMember(dm => dm.Items, mo => mo.MapFrom(sm =>
                    from child in sm.Items
                    select new OrderItemModel
                    {
                        SalesOrderId = sm.Id,
                        Id = child.Id,
                        Sku = child.Sku,
                        Quantity = child.Quantity,
                        WholeSalePrice = child.WholeSalePrice,
                        RetailPrice = child.RetailPrice,
                        DollarsOff = child.DollarsOff,
                        DiscountPercent = child.DiscountPercent,
                        Details = child.Details
                    }));


            CreateMap<Person, PersonModel>();
            CreateMap<Address, AddressModel>();
            CreateMap<CustomerReturn, CustomerReturnModel>();
        }
    }
}