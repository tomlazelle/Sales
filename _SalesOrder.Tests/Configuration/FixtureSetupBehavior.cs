using System;
using Fixie;
using Ploeh.AutoFixture.AutoNSubstitute;

namespace Sales.Tests.Configuration
{
    public class FixtureSetupBehavior : FixtureBehavior
    {
        public void Execute(Fixture context, Action next)
        {
            var fixture = new Ploeh.AutoFixture.Fixture().Customize(new AutoNSubstituteCustomization());

            
            context.Instance.GetType().TryInvoke("FixtureSetup", context.Instance, new object[]
            {
                fixture
            });


            next();

            context.Instance.GetType().TryInvoke("FixtureTearDown", context.Instance);
        }
    }
}