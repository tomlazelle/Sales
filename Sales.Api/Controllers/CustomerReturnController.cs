using System;
using System.Web.Http;
using Sales.Api.Models;

namespace Sales.Api.Controllers
{
    [RoutePrefix("v1/customerreturn")]
    public class CustomerReturnController
    {
        [Route("{salesOrderId}/{returnId}")]
        public CustomerReturnModel Get(Guid salesOrderId, string returnId)
        {
            return null;
        }
    }
}