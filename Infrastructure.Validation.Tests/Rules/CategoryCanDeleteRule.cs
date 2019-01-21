using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validation.Tests.Rules
{
    public class CategoryCanDeleteRule : IRule
    {
        Category _category;
        public CategoryCanDeleteRule(Category category)
        {
            _category = category;
        }
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
            get { return "ProductCanDelete"; }
        }

        public void Check()
        {
            //TODO : Check
            _isSucceed = true;
        }
    }
}
