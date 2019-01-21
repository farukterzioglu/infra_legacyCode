using System;
using System.IO;
using System.Linq;
using Infrastructure.WebApps.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure.PluginFramework.PluginManager.MVC;

namespace Infrastructure.PluginFramework.PluginManager.Tests
{
    [TestClass]
    public class PluginManagerTests
    {
        private IUserClaimsManager userClaimsManager;

        private class TestUserClaimsManager : IUserClaimsManager
        {
            public bool CheckUserClaim(string claim)
            {
                return true;
            }
        }

        [TestMethod]
        public void TestMethod1()
        {
            //if(!new DirectoryInfo(@"..\..\..\Infrastructure.PluginFramework.Core.Tests.Plugins\temp").Exists)
            //    Directory.CreateDirectory(@"..\..\..\Infrastructure.PluginFramework.Core.Tests.Plugins\temp");

            //Infrastructure.PluginFramework.PluginManager.MVC.PluginManager.Instance.SetContext(
            //    new PluginManagerContext(
            //        new TestUserClaimsManager(),
            //        new DirectoryInfo(@"..\..\..\Infrastructure.PluginFramework.Core.Tests.Plugins"),
            //        new DirectoryInfo(@"..\..\..\Infrastructure.PluginFramework.Core.Tests.Plugins\temp")));

            //Infrastructure.PluginFramework.PluginManager.MVC.PluginManager.Instance.InitializePlugins();

            //Assert.IsTrue(
            //    Infrastructure.PluginFramework.PluginManager.MVC.PluginManager.Instance.GetPluginNames().Any());
        }
    }
}
