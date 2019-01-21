using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Infrastructure.Validation.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Delete category
            Category category = new Category();

            Rules.CategoryDeleteValidation rule = new Rules.CategoryDeleteValidation(category);
            rule.Check();
        }
    }
}
