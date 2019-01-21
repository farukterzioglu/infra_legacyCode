using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.PluginFramework.Core.MVC
{
    public interface IComponent
    {
        string ComponentName { get; set; }
        string ActionName { get; set; }
        List<ComponentPlaceHolders> ApplyableTo { get; set; }
        bool IsListed { get; set; }
        bool IsDefault { get; set; }
    }

    public class Component : IComponent
    {
        public string ComponentName { get; set; }
        public string ActionName { get; set; }
        public List<ComponentPlaceHolders> ApplyableTo { get; set; }
        public bool IsListed { get; set; }
        public bool IsDefault { get; set; }
    }

    public class StaticComponent : IComponent
    {
        public string ComponentName { get; set; }
        public string ActionName { get; set; }
        public List<ComponentPlaceHolders> ApplyableTo { get; set; }
        public bool IsListed { get; set; }
        public bool IsDefault { get; set; }
        public string Content { get; set; }
    }
}
