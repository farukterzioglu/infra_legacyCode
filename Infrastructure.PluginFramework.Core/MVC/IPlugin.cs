using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.PluginFramework.Core.MVC
{
    public interface IPlugin
    {
        List<IPluginController> SubControllers { get; set; }
        string Name { get; set; }
        string PluginName { get; set; }
        string AreaName { get; set; }
        string IconClass { get; set; }
        bool IsImported { get; set; }
    }
}
