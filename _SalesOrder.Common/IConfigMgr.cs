using System;
using System.Configuration;

namespace Sales.Common
{
    public interface IConfigMgr
    {
        T Get<T>(string key);
    }

    public class ConfigMgr : IConfigMgr
    {
        public T Get<T>(string key)
        {
            var result = ConfigurationManager.AppSettings[key];

            return (T) Convert.ChangeType(result, typeof(T));
        }
    }
}