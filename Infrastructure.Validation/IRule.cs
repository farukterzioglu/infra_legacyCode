using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validation
{
    public interface IRule
    {
        bool IsSucceed { get;}
        string Name { get; }
        void Check();
    }
}
