using AutoFixture;

namespace SalesOrder.Tests.Configuration
{
    public interface ISubjectBase
    {
        void FixtureSetup(IFixture fixture);
        void FixtureTearDown();
    }
}