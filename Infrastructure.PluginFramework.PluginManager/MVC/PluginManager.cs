using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.PluginFramework.Core;
using Infrastructure.PluginFramework.Core.MVC;

namespace Infrastructure.PluginFramework.PluginManager.MVC
{
    public class PluginManager : IPluginManager
    {
        private static readonly Lazy<PluginManager> _instance =
            new Lazy<PluginManager>(() => new PluginManager());

        private static readonly List<IPlugin> Plugins;

        static PluginManager() 
        {
            Plugins = new List<IPlugin>();
        }

        public static PluginManager Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public List<IPlugin> GetAllPlugins()
        {
            return Plugins;
        }

        public void AddPlugin(IPlugin plugin)
        {
            Plugins.Add(plugin);
        }

        public List<IComponent> GetComponents(string pluginNameController)
        {
            string pluginName = pluginNameController.Split('/')[0];
            if (string.IsNullOrEmpty(pluginName)) return null;
            
            var firstOrDefault = Plugins.FirstOrDefault(x => x.PluginName == pluginName);
            if (firstOrDefault != null)
            {
                string controllerName = pluginNameController.Split('/')[1];
                if (string.IsNullOrEmpty(controllerName)) return null;

                var controller = firstOrDefault
                    .SubControllers
                    .FirstOrDefault(x => x.ControllerName == controllerName);

                if (controller != null)
                    return controller.Components;
            }

            return new List<IComponent>();
        }
    }
}
