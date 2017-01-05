namespace Sales.Api.Models.Handlers
{
    public class GetAllLink : Link
    {
        public GetAllLink(string href) : base(href, "collection", "GetAll")
        {
        }
    }
}