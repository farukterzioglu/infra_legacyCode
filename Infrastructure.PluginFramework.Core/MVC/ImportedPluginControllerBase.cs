using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Infrastructure.PluginFramework.Core.MVC
{
    public abstract class ImportedPluginControllerBase : PluginControllerBase
    {
        protected override ActionResult CombineComponents(List<IComponent> components, object model)
        {
            string content =
                components.Aggregate("", (current, component) => current + ((StaticComponent)component).Content);

            return Content(content);
        }
    }

    public class ImportedPluginController : ImportedPluginControllerBase
    {
        public override string Title { get; set; }
        public override string ControllerName { get; set; }

        public override List<IComponent> Components
        {
            get { throw new NotImplementedException(); }
        }
    }
}
