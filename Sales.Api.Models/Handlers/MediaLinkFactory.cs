using System.Web.Http.Routing;

namespace Sales.Api.Models.Handlers
{
    public class MediaLinkFactory : IMediaLinkFactory
    {
        private readonly UrlHelper _helper;

        public MediaLinkFactory(UrlHelper helper)
        {
            _helper = helper;
        }

        public Link CreateLink(MediaLinkType linkType, string controllerName, object values)
        {
            if (linkType == MediaLinkType.Self)
            {
                return CreateGetLink(NormalizeControllerName(controllerName), values);
            }

            if (linkType == MediaLinkType.Edit)
            {
                return CreateEditLink(NormalizeControllerName(controllerName), values);
            }

            if (linkType == MediaLinkType.GetAll)
            {
                return CreateGetAllLink(NormalizeControllerName(controllerName), values);
            }
            return CreateLink(controllerName, values, "Get", "Self");
        }

        private Link CreateGetAllLink(string controllerName, object values)
        {
            return new GetAllLink(_helper.Link(RouteName(controllerName, "GetAll"), values));
        }

        private Link CreateEditLink(string controllerName, object values)
        {
            return new EditLink(_helper.Link(RouteName(controllerName, "Put"), values));
        }

        public Link CreateLink(string controllerName, object values, string method, string relation)
        {
            return CreateDefaultLink(NormalizeControllerName(controllerName), values, method, relation);
        }

        private Link CreateDefaultLink(string controllerName, object values, string method, string relation)
        {
            return new DefaultLink(_helper.Link(RouteName(controllerName, method), values), relation, method);
        }

        private string NormalizeControllerName(string controllerName)
        {
            if (controllerName.EndsWith("Controller"))
            {
                return controllerName.Replace("Controller", "");
            }

            return controllerName;
        }

        private Link CreateGetLink(string controllerName, object values)
        {
            return new SelfLink(_helper.Link(RouteName(controllerName, "Get"), values));
        }

        private string RouteName(string prefix, string verb)
        {
            var pathValues = _helper.Request.RequestUri.AbsolutePath.Split(char.Parse("/"));

            var version = pathValues[1];

            var result = $"{prefix}{version}{verb}";

            return result;
        }
    }
}