using EventSource.Framework;

namespace Sales.Common
{
    public class DummyPublisher : IEventPublisher
    {
        public void Publish<TMessage>(TMessage message)
        {
        }
    }
}