using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Infrastructure.PluginFramework.Core.MVC
{
    public abstract class PluginControllerBase : Controller, IPluginController
    {
        //Protected Abstract
        public abstract List<IComponent> Components { get; }

        protected static IComponent ErrorPageComponent = new Component()
        {
            ApplyableTo = new List<ComponentPlaceHolders>
            {
                ComponentPlaceHolders.MainContent,
                ComponentPlaceHolders.SideContent
            },
            ActionName = "ComponentErrorPage",
            ComponentName = "Component Error Page",
            IsListed = false,
            IsDefault = false
        };

        protected ActionResult ComponentErrorPage()
        {
            return null;
        }
        protected virtual ActionResult CombineComponents(List<IComponent> components, object model)
        {
            var multipleViewResult =
                new MultipleViewResult(components.Select(x => PartialView(((Component)x).ActionName, model)).ToList());
            return multipleViewResult;
        }

        //Private
        private bool Authorize(IComponent component)
        {
            string claim = this.ControllerName + "/" + component.ActionName;
            //TODO : Implement component authorization
            //Return both authorized add currently being used

            return false;
        }
        private List<IComponent> GetAuthorizedComponents(List<IComponent> components)
        {
            return components.Where(Authorize).ToList();
        }
        private ActionResult PrepareComponents(ComponentPlaceHolders placeholder, object model = null)
        {
            var components = new List<IComponent>();

            //Get Components 
            var allComponents = Components;
            if (allComponents == null || !allComponents.Any()) return null;

            allComponents = allComponents.Where(x => x.ApplyableTo.Contains(placeholder)).ToList();

            //Add default components 
            components.AddRange(allComponents.Where(x => x.IsDefault).ToList());

            //Add authorized components 
            components.AddRange(
                GetAuthorizedComponents(allComponents.Except(components).ToList()).ToList());

            //Add error page 
            components.Add(ErrorPageComponent);

            return CombineComponents(components, model);
        }

        //Public (IPluginController)
        public abstract string Title { get; set; }
        public abstract string ControllerName { get; set; }

        //public ActionResult ComponentsMenu()
        //{
        //    var allComponents = GetAuthorizedComponents(GetComponents()).ToList();
        //    //Get Components 
        //    if (!allComponents.Any()) return null;

        //    return View()
        //    return RedirectToAction("ComponentsMenu", "Home", new { area = "" });
        //        //allComponents.Where(x => !x.IsDefault && x.IsListed).ToList());
        //}

        public ActionResult MainContent(object model = null)
        {
            return PrepareComponents(ComponentPlaceHolders.MainContent, model);
        }
        public ActionResult WidgetsContent(object model = null)
        {
            return PrepareComponents(ComponentPlaceHolders.WidgetsContent, model);
        }
        public ActionResult SideContent(object model = null)
        {
            return PrepareComponents(ComponentPlaceHolders.SideContent, model);
        }
        public ActionResult ShortcutsContent(object model = null)
        {
            return PrepareComponents(ComponentPlaceHolders.ShortcutsContent, model);
        }
    }
}
