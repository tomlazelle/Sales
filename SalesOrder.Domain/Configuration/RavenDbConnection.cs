using Microsoft.Extensions.Options;

namespace SalesOrder.Domain.Configuration
{
    public class RavenDbConnection : IRavenDbConnection
    {
        public RavenDbConnection(IOptions<RavenDbSettings> settings)
        {
            Url = settings.Value.Url;
            DefaultDatabase = settings.Value.DefaultDatabase;
        }

        public string Url { get; set; }
        public string DefaultDatabase { get; set; }
    }

    public class RavenDbSettings
    {
        [Newtonsoft.Json.JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "default-database")]
        public string DefaultDatabase { get; set; }
    }
}