using System;
using System.Linq;
using Fixie;

namespace Sales.Tests.Configuration
{
    public class TestConvention : Convention
    {
        public TestConvention()
        {
            FixtureExecution.Wrap<FixtureSetupBehavior>();

            Classes
                .NameEndsWith("test", "Test");

            ClassExecution
                .CreateInstancePerClass();


            Methods
                .Where(
                    x => x.IsVoid() &&
                         x.IsPublic &&
                         x.CustomAttributes.All(a => a.AttributeType.Name != "IgnoreAttribute") &&
                         x.Name != "FixtureSetup" &&
                         x.Name != "FixtureTearDown" &&
                         x.Name != "RegisterDatabase");


            CaseExecution.Wrap<RepeatCase>();
        }
    }

    public class RepeatCase : CaseBehavior
    {
        public void Execute(Case context, Action next)
        {
            var attribs = context.Method.GetCustomAttributes(typeof(RepeatAttribute), false);

            var max = 1;

            if (attribs != null && attribs.Any())
            {
                max = ((RepeatAttribute)attribs[0]).Count;
            }

            for (int i = 0; i < max; i++)
            {
                Console.WriteLine($"Running Test {i + 1} of {max}");
                next();
            }
        }
    }
    public class RepeatAttribute : Attribute
    {
        public RepeatAttribute(int count)
        {
            Count = count;
        }

        public int Count { get; set; }
    }
}