using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validation.Tests.Rules
{
    public class CategoryDeleteValidation : ValidationBase
    {
        private Category _category { get; set; }
        private List<IRule> Rules{ get; set; }

        public CategoryDeleteValidation(Category category)
        {
            _category = category;
        }

        public void Check() {
            CategoryCanDeleteRule rule = new CategoryCanDeleteRule(_category);
            rule.Check();

            //Get product of category 
            List<Product> list = new List<Product>();
            list.ForEach(x =>
            {
                new ProductCanDeleteRule(x);
            });
        }
    }
}
