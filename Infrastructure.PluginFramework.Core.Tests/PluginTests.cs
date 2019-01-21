using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure.PluginFramework.Core.MVC;

namespace Infrastructure.PluginFramework.Core.Tests
{
    public static class TestConsts
    {
        public static string TestPluginName = "Test Plugin";
        public static string TestControllerTitle = "Test Controller Title";
        public static string TestControllerLayout = "Test Controllr Layout";
        public static string TestControllerContent = "Test Content";
    }

    public class Sample1Plugin : IPlugin
    {
        public List<PluginControllerBase> SubControllers { get; set; }
        public string Name { get; set; }
        public string AreaName { get; set; }
        public string IconClass { get; set; }

        List<IPluginController> IPlugin.SubControllers
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string PluginName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsImported
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
    //public class TestPlugin : Infrastructure.PluginFramework.Core.IPlugin
    //{
    //    public TestPlugin()
    //    {
    //        ControllerTypes = new List<Type>();
    //        ControllerTypes.Add(typeof(TestController));
    //    }

    //    public List<Type> ControllerTypes { get; set; }
    //    public string PluginName {
    //        get { return TestConsts.TestPluginName; }
    //    }
    //}
    
    public class TestController : PluginControllerBase
    {
        public override List<IComponent> Components
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string ControllerName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        //public override Type PluginType
        //{
        //    get { return typeof(Sample1Plugin); }
        //}

        //public override string Layout {
        //    get { return TestConsts.TestControllerLayout; }
        //    set { }
        //}
        public override string Title {
            get { return TestConsts.TestControllerTitle; }
            set { }
        }
        
        //protected override ActionResult OnIndex()
        //{
        //    return new ContentResult() {Content = TestConsts.TestControllerContent};
        //}
    }

    [TestClass]
    public class PluginTests
    {
        [TestMethod]
        public void TestPluginInstance()
        {
            Sample1Plugin testPlugin = new Sample1Plugin();

            Assert.AreEqual(TestConsts.TestPluginName, testPlugin.PluginName);
            //Assert.IsTrue(testPlugin.ControllerTypes.Any());
        }

        [TestMethod]
        public void TestPluginControllerInstance()
        {
            TestController testController = new TestController();

            //Assert.AreEqual(testController.PluginType, typeof(Sample1Plugin));
            Assert.AreEqual(testController.Title, TestConsts.TestControllerTitle);
            //Assert.AreEqual(testController.Layout, TestConsts.TestControllerLayout);
            //Assert.AreEqual(((ContentResult)testController.Index()).Content, TestConsts.TestControllerContent);
        }

        [TestMethod]
        public void TestPluginWithControllers()
        {
            Sample1Plugin testPlugin = new Sample1Plugin();
            //testPlugin.ControllerTypes.Add(typeof(TestController));

            //Assert.IsTrue(testPlugin.ControllerTypes.Any());
        }
    }
}
