namespace Sales.Api.Models.Handlers
{
    public class Link
    {
        public string Rel { get; private set; }
        public string Href { get; private set; }
        public string Method { get; private set; }

        public Link(string href, string relation, string method = null)
        {
            Rel = relation;
            Href = href;
            Method = method;
        }

        protected Link()
        {
        }
    }
}