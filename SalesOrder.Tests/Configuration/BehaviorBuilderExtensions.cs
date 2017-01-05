using System;
using System.Reflection;
using Fixie;

namespace Sales.Tests.Configuration
{
    public static class BehaviorBuilderExtensions
    {
        public static void TryField(this Fixture context, string fieldName, object fieldValue)
        {
            var lifecycleMethod = context.Class.Type.GetField(fieldName, BindingFlags.Public | BindingFlags.Instance);

            if (lifecycleMethod == null)
                return;

            try
            {
                lifecycleMethod.SetValue(context.Instance, fieldValue);
            }
            catch (TargetInvocationException exception)
            {
                throw new PreservedException(exception.InnerException);
            }
        }

        public static void TryProperty(this Fixture context, string propertyName, object propertyValue)
        {
            var lifecycleMethod = context.Class.Type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

            if (lifecycleMethod == null)
                return;

            try
            {
                lifecycleMethod.SetValue(context.Instance, propertyValue);
            }
            catch (TargetInvocationException exception)
            {
                throw new PreservedException(exception.InnerException);
            }
        }

        public static void TryInvoke(this Type type, string method, object instance, object[] paramObjects = null)
        {
            var lifecycleMethod = type.GetMethod(method);

            //                type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
            //                    .SingleOrDefault(x => 
            //                    paramObjects == null? ReflectionExtensions.HasSignature(x, typeof(void), method):
            //                    ReflectionExtensions.HasSignature(x, typeof(void), method,paramObjects.Select(t=>t.GetType()).ToArray()));

            if (lifecycleMethod == null)
                return;

            try
            {
                lifecycleMethod.Invoke(instance, paramObjects);
            }
            catch (TargetInvocationException exception)
            {
                throw new PreservedException(exception.InnerException);
            }
        }
    }
}