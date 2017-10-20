using System;
using SalesOrder.Api.Models.Handlers;

namespace SalesOrder.Api.Models
{
    public class SalesOrderCreatedModel:Resource
    {
        public Guid Id { get; set; }
    }
}