using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Internal;
using SSS.Api.Seedwork.ServiceCollection;

namespace SSS.Api.Seedwork.Controller
{
    public class BaseControllerActivator : DefaultControllerActivator
    {
        public BaseControllerActivator(ITypeActivatorCache typeActivatorCache)
            : base(typeActivatorCache) { }

        public override object Create(ControllerContext controllerContext)
        {
            var controller = base.Create(controllerContext);
            controllerContext.HttpContext.RequestServices.Autowired(controller);
            return controller;
        }
    }
}
