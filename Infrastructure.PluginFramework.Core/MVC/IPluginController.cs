using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Infrastructure.PluginFramework.Core.MVC
{
    public interface IPluginController
    {
        string Title { get; set; }
        string ControllerName { get; set; }
        List<IComponent> Components { get; }

        ActionResult MainContent(object model = null);
        ActionResult SideContent(object model = null);
        ActionResult WidgetsContent(object model = null);
        ActionResult ShortcutsContent(object model = null);
    }
}
