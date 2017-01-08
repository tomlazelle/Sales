using Sales.Common;

namespace Sales.Domain.Configuration
{
    public class RavenDbConnection : IRavenDbConnection
    {
        public RavenDbConnection(IConfigMgr configManager)
        {
            var environment = configManager.Get<string>("environment");

            Url = configManager.Get<string>($"{environment}-raven-url");
            DefaultDatabase = configManager.Get<string>($"{environment}-raven-default-database");
        }

        public string Url { get; set; }
        public string DefaultDatabase { get; set; }
    }
}