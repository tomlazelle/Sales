namespace Sales.Domain.Configuration
{
    public interface IRavenDbConnection
    {
        string Url { get; set; }
        string DefaultDatabase { get; set; }
    }
}