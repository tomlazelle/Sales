using System;
using Sales.Api.Models.Handlers;

namespace Sales.Api.Models
{
    public class SalesOrderCreatedModel:Resource
    {
        public Guid Id { get; set; }
    }
}