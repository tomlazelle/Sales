namespace Sales.Api.Models.Handlers
{
    public interface IMediaLinkFactory
    {
        Link CreateLink(MediaLinkType linkType, string controllerName, object values);
        Link CreateLink(string controllerName, object values, string method, string relation);
    }
}