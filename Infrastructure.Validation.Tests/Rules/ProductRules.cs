using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validation.Tests.Rules
{
    public class ProductCanDeleteRule : IRule
    {
        private Product x;
        private bool _isSucceed;

        public ProductCanDeleteRule(Product x)
        {
            this.x = x;
        }

        public bool IsSucceed
        {
            get
            {
                return _isSucceed;
            }
        }

        public string Name
        {
            get { return "ProductCanDelete"; }
        }

        public void Check()
        {
            //TODO : Check
            _isSucceed = true;
        }
    }

    public class NameNotNullRule : IRule
    {
        private bool _isSucceed;

        public bool IsSucceed
        {
            get
            {
                return _isSucceed;
            }
        }

        public string Name
        {
            get {return "NameNotNullRule";}
        }

        public void Check()
        {
            //TODO : Check
            _isSucceed = true;
        }
    }
}
