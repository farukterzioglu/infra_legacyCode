using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.PluginFramework.Core.MVC
{
    public interface IPluginManager
    {
        List<IPlugin> GetAllPlugins();
        void AddPlugin(IPlugin plugin);
    }
}
