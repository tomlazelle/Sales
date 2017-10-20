namespace Sales.Api.Models.Handlers
{
    public class DefaultLink : Link
    {
        public DefaultLink(string href, string relation = "self", string method = null) : base(href, relation, method)
        {
        }
    }
}