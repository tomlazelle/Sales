namespace SalesOrder.Domain.Configuration
{
    public interface IRavenDbConnection
    {
        string Url { get; set; }
        string DefaultDatabase { get; set; }
    }
}