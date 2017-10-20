using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalesOrder.Api.Models;
using SalesOrder.Domain.Handlers;
using SalesOrder.Domain.Messages;

namespace Sales.Api.Controllers
{
    [Route("v1/salesorder")]
    [Produces("application/json")]
    public class SalesOrderController : Controller
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

        [Route("")]
        [HttpPost]
        public SalesOrderCreatedModel Post([FromBody]CreateSalesOrderMessage createSalesOrderMessage)
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

        [Route("")]
        [HttpGet]
        public IEnumerable<SalesOrderModel> Get(int pageIndex, int itemsPerPage)
        {
            var salesOrders = _salesOrderQueryHandler.Get(pageIndex, itemsPerPage);

            var result = _mapper.Map<IEnumerable<SalesOrder.Domain.Aggregates.SalesOrder>, IEnumerable<SalesOrderModel>>(salesOrders);

            return result;
        }

        [Route("id")]
        [HttpGet]
        public SalesOrderModel Get(Guid id)
        {
            var salesOrders = _salesOrderQueryHandler.Get(id);

            var result = _mapper.Map<SalesOrder.Domain.Aggregates.SalesOrder, SalesOrderModel>(salesOrders);

            return result;
        }

        [Route("{id}/status")]
        [HttpPut]
        public SalesOrderModel Status(Guid id,[FromBody] UpdateReturnStatusMessage message)
        {
            var salesOrder = _handler.Handle(message);

            var result = _mapper.Map<SalesOrder.Domain.Aggregates.SalesOrder, SalesOrderModel>(salesOrder);

            return result;
        }
    }
}