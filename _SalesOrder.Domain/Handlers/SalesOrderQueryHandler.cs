using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Client.Linq;
using Sales.Domain.Aggregates;

namespace Sales.Domain.Handlers
{
    public class SalesOrderQueryHandler
    {
        private readonly IDocumentStore _documentStore;

        public SalesOrderQueryHandler(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public IList<SalesOrder> Get(int pageIndex, int itemsPerPage)
        {
            IList<SalesOrder> salesOrders;

            using (var session = _documentStore.OpenSession())
            {
                RavenQueryStatistics stats;

                salesOrders = session
                    .Query<SalesOrderEvents>()
                    .Customize(x => x.WaitForNonStaleResults(TimeSpan.FromSeconds(5)))
                    .Statistics(out stats)
                    .Skip((pageIndex - 1) * itemsPerPage)
                    .Take(itemsPerPage)
                    .ToList()
                    .Select(x => new SalesOrder(x.Id, x))
                    .ToList();
            }

            return salesOrders;
        }

        public SalesOrder Get(Guid id)
        {
            using (var session = _documentStore.OpenSession())
            {
                var events = session.Load<SalesOrderEvents>("SalesOrderEvents/" + id);

                var salesOrder = new SalesOrder(id, events);
                return salesOrder;
            }
        }
    }
}