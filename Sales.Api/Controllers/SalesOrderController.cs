using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using Sales.Api.Models;
using Sales.Domain.Aggregates;
using Sales.Domain.Handlers;
using Sales.Domain.Messages;

namespace Sales.Api.Controllers
{
    [RoutePrefix("v1/salesorder")]
    public class SalesOrderController : ApiController
    {
        private readonly SalesOrderHandler _handler;
        private readonly IMapper _mapper;
        private readonly SalesOrderQueryHandler _salesOrderQueryHandler;

        public SalesOrderController(
            SalesOrderHandler handler,
            SalesOrderQueryHandler salesOrderQueryHandler,
            IMapper mapper)
        {
            _handler = handler;
            _salesOrderQueryHandler = salesOrderQueryHandler;
            _mapper = mapper;
        }

        [Route()]
        public SalesOrderCreatedModel Post(CreateSalesOrderMessage createSalesOrderMessage)
        {
            if (createSalesOrderMessage.Id == Guid.Empty)
            {
                createSalesOrderMessage.Id = Guid.NewGuid();
            }

            var result = _handler.Handle(createSalesOrderMessage);


            return new SalesOrderCreatedModel
            {
                Id = result.Id
            };
        }

        [Route]
        public IEnumerable<SalesOrderModel> Get(int pageIndex, int itemsPerPage)
        {
            var salesOrders = _salesOrderQueryHandler.Get(pageIndex, itemsPerPage);

            var result = _mapper.Map<IEnumerable<SalesOrder>, IEnumerable<SalesOrderModel>>(salesOrders);

            return result;
        }

        [Route("id")]
        public SalesOrderModel Get(Guid id)
        {
            var salesOrders = _salesOrderQueryHandler.Get(id);

            var result = _mapper.Map<SalesOrder, SalesOrderModel>(salesOrders);

            return result;
        }
    }
}