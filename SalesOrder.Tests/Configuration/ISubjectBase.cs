using Ploeh.AutoFixture;

namespace Sales.Tests.Configuration
{
    public interface ISubjectBase
    {
        void FixtureSetup(IFixture fixture);
        void FixtureTearDown();
    }
}