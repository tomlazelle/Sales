namespace Sales.Api.Models.Handlers
{
    public class SelfLink : Link
    {
        public SelfLink(string href) : base(href, "self", "Get")
        {
        }
    }
}