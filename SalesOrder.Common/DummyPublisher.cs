using EventSource.Framework;

namespace SalesOrder.Common
{
    public class DummyPublisher : IEventPublisher
    {
        public void Publish<TMessage>(TMessage message)
        {
        }
    }
}