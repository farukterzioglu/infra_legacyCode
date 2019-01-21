using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Infrastructure.AspectOriented.Aspects.ActionFilters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ProfileFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);   
        }
    }
}
