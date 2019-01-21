using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.WebApps.Common;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Infrastructure.AspectOriented.Aspects
{
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException() : base() {}

        public NotAuthorizedException(string message)
            : base(message) { }

        public NotAuthorizedException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public NotAuthorizedException(string message, Exception innerException)
            : base(message, innerException) { }

        public NotAuthorizedException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }
    }

    //Authorization Handler
    public class AuthorizeAspect : HandlerAttribute
    {
        private readonly string _claim;
        private readonly string[] _claims;
        
        //TODO : Don't send user claims
        //TODO : How to catch method/class details
        public AuthorizeAspect(string currentClaim, string[] userClaims)
        {
            _claim = currentClaim;
            _claims = userClaims;
        }

        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new AuthorizeAspectCallHandler(container, _claim, _claims.ToList());
        }
    }
    public class AuthorizeAspectCallHandler : ICallHandler
    {
        private readonly string _claim;
        private readonly List<string> _claims;
        
        //TODO : Don't send user claims
        public AuthorizeAspectCallHandler(IUnityContainer container, string currentClaim, List<string> userClaims)
        {
            _claim = currentClaim;
            _claims = userClaims;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            Console.WriteLine("Checking authorization before invoke.");

            //Get users claim now and check
            //TODO : How to check claims???
            if (_claims.Contains(_claim))
                Console.WriteLine("User authorized.");
            else
            {
                Console.WriteLine("User not authorized.");
                
                throw new NotAuthorizedException();
            }

            var result = getNext().Invoke(input, getNext);

            Console.WriteLine("Authorized invocation completed.");

            return result;
        }

        public int Order { get; set; }
    }

    //Authorization Behaviors
    public class PluginControllerAuthorizationBehavior : IInterceptionBehavior
    {
        private IUserClaimsManager _userClaimsManager;
        private IUserClaimsManager UserClaimsManager
        {
            get
            {
                if (_userClaimsManager == null)
                {
                    //try
                    //{
                    //    //TODO : Inject IUserClaimsManager
                    //    //_userClaimsManager = IoCManager.Instance.ResolveIfRegistered<IUserClaimsManager>();

                    //}
                    ////TODO : Why NotAuthorizedException???
                    ////catch (NotRegisteredException ex)
                    ////{
                    ////    throw new NotAuthorizedException("Claim manager couldn't found.");
                    ////}

                    if (_userClaimsManager == null)
                    {
                        //TODO : Log exception
                        throw new NotAuthorizedException("Claim manager couldn't found.");
                    }
                }
                return _userClaimsManager;
            }
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            //todo : Implement this 
            throw new NotImplementedException();

            ////Method name
            //var methodBase = input.MethodBase;

            ////Plugin Controller Name
            //var pluginControllerType = input.Target.GetType();
            //IPluginController pluginControllerInstance = Activator.CreateInstance(pluginControllerType) as PluginControllerBase;

            //if (pluginControllerInstance == null)
            //    throw new InvalidCastException("Couldn't cast to IPluginController");
            ////var pluginControllerName = pluginControllerInstance.AssemblyName;
            //var pluginControllerName = pluginControllerType.Name;

            ////Plugin Name
            //var pluginType = pluginControllerInstance.PluginType;
            //var pluginName = pluginType.Name;

            ////Create claim
            ////TODO : Add method ?? InterceptionBehavior cause problem, searches for get_title claim
            //string claim = pluginName + "/" + pluginControllerName;// + "/" + methodBase.Name; 

            ////Check claim
            //if (!UserClaimsManager.CheckUserClaim(claim))
            //    throw new NotAuthorizedException("User doesn't have " + claim + " claim.");

            //// Invoke the next behavior in the chain.
            //var result = getNext()(input, getNext);

            
            //// After invoking the method on the original target.
            //if (result.Exception != null)
            //{
            //    var temp = result.Exception.Message;
            //}
            //else
            //{
            //    var temp = result.ReturnValue;
            //}

            //return result;
            return null;
        }

        //https://msdn.microsoft.com/en-us/library/dn178466(v=pandp.30).aspx
        //The GetRequiredInterfaces method enables you to specify the interface types that you want to associate with the behavior. 
        //In this example, the interceptor registration will specify the interface type, and therefore the GetRequiredInterfaces method returns Type. 
        //EmptyTypes. For more information about how to use these two methods, see the topic : https://unity.codeplex.com/downloads/get/669364
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }
        public bool WillExecute
        {
            get { return true; }
        }
    }
    public class PluginAuthorizationBehavior : IInterceptionBehavior
    {
        private IUserClaimsManager _userClaimsManager;
        private IUserClaimsManager UserClaimsManager
        {
            get
            {
                if (_userClaimsManager == null)
                {
                    //TODO : Inject IUserClaimsManager
                    //TODO : NotRegisteredException -> NotAuthorizedException ???
                    //try
                    //{
                    //    _userClaimsManager = IoCManager.Instance.ResolveIfRegistered<IUserClaimsManager>();

                    //}
                    //catch (NotRegisteredException ex)
                    //{
                    //    throw new NotAuthorizedException("Claim manager couldn't found.");
                    //}

                    if (_userClaimsManager == null)
                    {
                        //TODO : Log exception
                        throw new NotAuthorizedException("Claim manager couldn't found.");
                    }
                }
                return _userClaimsManager;
            }
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            //Method name
            var methodBase = input.MethodBase;

            //Plugin Controller Name
            var pluginType = input.Target.GetType();
            var pluginTypeName = pluginType.Name;

            //Create claim
            string claim = pluginTypeName;

            //Check claim
            if (!UserClaimsManager.CheckUserClaim(claim))
                throw new NotAuthorizedException("User doesn't have " + claim + " claim.");

            // Invoke the next behavior in the chain.
            var result = getNext()(input, getNext);


            // After invoking the method on the original target.
            if (result.Exception != null)
            {
                var temp = result.Exception.Message;
            }
            else
            {
                var temp = result.ReturnValue;
            }

            return result;
        }

        //https://msdn.microsoft.com/en-us/library/dn178466(v=pandp.30).aspx
        //The GetRequiredInterfaces method enables you to specify the interface types that you want to associate with the behavior. 
        //In this example, the interceptor registration will specify the interface type, and therefore the GetRequiredInterfaces method returns Type. 
        //EmptyTypes. For more information about how to use these two methods, see the topic : https://unity.codeplex.com/downloads/get/669364
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }
        public bool WillExecute
        {
            get { return true; }
        }
    }
}
