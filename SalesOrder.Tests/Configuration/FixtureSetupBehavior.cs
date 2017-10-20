using System;
using AutoFixture.AutoNSubstitute;
using Fixie;

namespace SalesOrder.Tests.Configuration
{
    public class FixtureSetupBehavior : FixtureBehavior
    {
        public void Execute(Fixture context, Action next)
        {
            var fixture = new AutoFixture.Fixture().Customize(new AutoNSubstituteCustomization());

            
            context.Instance.GetType().TryInvoke("FixtureSetup", context.Instance, new object[]
            {
                fixture
            });


            next();

            context.Instance.GetType().TryInvoke("FixtureTearDown", context.Instance);
        }
    }
}