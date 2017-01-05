namespace Sales.Api.Models.Handlers
{
    public class EditLink : Link
    {
        public EditLink(string href) : base("edit", href, "Put")
        {
        }
    }
}