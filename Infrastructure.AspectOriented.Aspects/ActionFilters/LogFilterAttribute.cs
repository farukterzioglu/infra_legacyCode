using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Infrastructure.AspectOriented.Aspects.ActionFilters
{
    public enum Importance
    {
        Low,
        Medium,
        High
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class LogFilterAttribute : ActionFilterAttribute
    {
        public Importance Importance;
        //public ILoger Loger;

        public LogFilterAttribute(Importance importance = Importance.Medium)
        {
            Importance = importance;
        }

        public LogFilterAttribute()
        {
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
    }
}
