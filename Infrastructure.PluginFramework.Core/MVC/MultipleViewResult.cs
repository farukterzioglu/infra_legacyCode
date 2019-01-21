using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Infrastructure.PluginFramework.Core.MVC
{
    public class MultipleViewResult : ActionResult
    {
        public IList<PartialViewResult> PartialViewResults { get; private set; }

        public MultipleViewResult(List<PartialViewResult> views)
        {
            if (PartialViewResults == null)
                PartialViewResults = new List<PartialViewResult>();
            foreach (var v in views)
                PartialViewResults.Add(v);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            var total = PartialViewResults.Count;
            for (var index = 0; index < total -1; index++)
            {
                try
                {
                    var pv = PartialViewResults[index];
                    pv.ExecuteResult(context);

                }
                catch (Exception ex)
                {
                    var pv = PartialViewResults[total-1];
                    pv.ViewBag.ErrorMessage = ex.Message;
                    pv.ExecuteResult(context);
                }
            }
        }
    }
}
