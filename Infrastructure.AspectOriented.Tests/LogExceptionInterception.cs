using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure.CrossCutting.Log;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Infrastructure.Aspects.HandlerAttributes;
using Infrastructure.Domain.CrossCutting.Log;

namespace Infrastructure.AspectOriented.Tests
{
    public interface IClassToIntercept
    {
        void DoWorkWithException();

        /// <summary>
        /// Logs exception for all implementations 
        /// </summary>
        [LogException]
        void DoWorkWithExceptionWithLogging();
    }

    public class ClassToIntercept : IClassToIntercept
    {
        /// <summary>
        /// Logs exception
        /// </summary>
        [LogException]
        public void DoWorkWithException()
        {
            throw new NotImplementedException();
        }

        public void DoWorkWithExceptionWithLogging()
        {
            throw new NotImplementedException();
        }
    }

    [TestClass]
    public class LogExceptionInterception
    {
        private static bool _isLogerCalled = false; //Test specific
        private IUnityContainer _container;

        //Used inside interceptor
        public class ConsoleLoger : ILogger
        {
            public void Log(Audit audit)
            {
                throw new NotImplementedException();
            }
            
            public void Log(LogMessage message, Category category = Category.Exception, Priority priority = Priority.Medium, ExceptionDetails exceptionDetails = null)
            {
                Console.WriteLine(message.Message);
                _isLogerCalled = true;
            }
        }

        [TestInitialize]
        public void Init()
        {
            _container = new UnityContainer();

            //Add interception extension
            _container.AddNewExtension<Interception>(); 

            _container.RegisterType<ILogger, ConsoleLoger>();
            _container.RegisterType<IClassToIntercept, ClassToIntercept>();

            //Configure interception
            _container.Configure<Interception>().SetInterceptorFor<IClassToIntercept>(new InterfaceInterceptor());
        }
        
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void LogExceptionWithAttribute()
        {
            try
            {
                IClassToIntercept newClass = _container.Resolve<IClassToIntercept>();
                newClass.DoWorkWithException();
            }
            catch (Exception)
            {
                Assert.IsTrue(_isLogerCalled);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void LogExceptionWithAttributeFromInterface()
        {
            try
            {
                IClassToIntercept newClass = _container.Resolve<IClassToIntercept>();
                newClass.DoWorkWithExceptionWithLogging();
            }
            catch (Exception)
            {
                Assert.IsTrue(_isLogerCalled);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void InterceptWithoutInterfaceResolving()
        {
            try
            {
                _isLogerCalled = false;

                _container = new UnityContainer();
                _container.RegisterType<ClassToIntercept>();
                //_container.Configure<Interception>().SetInterceptorFor<ClassToIntercept>(new InterfaceInterceptor());

                ClassToIntercept classToIntercept = _container.Resolve<ClassToIntercept>();
                classToIntercept.DoWorkWithExceptionWithLogging();
            }
            catch (Exception)
            {
                Assert.IsTrue(_isLogerCalled);
                throw;
            }
        }


    }
}
