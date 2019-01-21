using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validation.Tests.Rules
{
    public class ProductDeleteFactory
    {
        List<IRule> _rules;
        public ProductDeleteFactory()
        {
            _rules = new List<IRule>();
        }
    }
}
