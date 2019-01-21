using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Infrastructure.CrossCutting.Cache;
using Infrastructure.AspectOriented.Aspects.HandlerAttributes;

namespace Infrastructure.AspectOriented.Aspects.Handlers
{
    public class CachingBehavior : ICallHandler
    {
        /// <summary>
        /// Implement this method to execute your behavior processing.
        /// </summary>
        /// <param name="input">Inputs to the current call to the target.</param>
        /// <param name="getNext">Delegate to execute to get the next delegate 
        /// in the behavior chain</param>
        /// <returns>
        /// Return value from the target.
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            //TODO : Move caching to crocccutting and inject ICacher to here 
            throw new NotImplementedException();

            //Console.WriteLine();
            //Console.WriteLine();
            //ObjectCache cache = MemoryCache.Default;
            //string ClassMethodName = string.Format("{0}::{1}", input.MethodBase.ReflectedType.Name, input.MethodBase.Name);
            //Console.WriteLine(string.Format("**** Caching Behaviour Executed {0} *****", ClassMethodName));
            //string ParameterInfo = GetParameterInfo(input);
            //string HashCacheKeyValue = HashCacheKey(string.Format("{0}:{1}", ClassMethodName, ParameterInfo));
            //Console.WriteLine(string.Format("HashCacheKeyValue = {0}", HashCacheKeyValue));
            ////If cash was not set or if it has expired then no object is returned
            //var CashedObject = cache.Get(HashCacheKeyValue, null);
            //Stopwatch stopwatch = new Stopwatch();
            //// Begin timing
            //stopwatch.Start();
            //MyXmlSerializer xmlSer = new MyXmlSerializer();
            //if (CashedObject != null)
            //{
            //    Console.WriteLine("Value from Cache...");
            //    stopwatch.Stop();
            //    Console.WriteLine("***** Time elapsed Executing {0} => {1} *******", ClassMethodName, stopwatch.Elapsed);
            //    var returnType = ((MethodInfo)input.MethodBase).ReturnType;
            //    return input.CreateMethodReturn(xmlSer.Deserialize(returnType, CashedObject));
            //}
            //else
            //{
            //    CacheAttribute cacheAttribute = GetCacheSettings(input);
            //    Console.WriteLine(string.Format("Value from DB...Total Minutes = {0}, CacheType = {1}, SerilizationType = {2}",
            //        cacheAttribute.Lifespan.TotalMinutes, cacheAttribute.CacheType, cacheAttribute.SerializationFormat));
            //    var msg = getNext()(input, getNext);

            //    if (!cacheAttribute.Disabled)
            //    {
            //        var cacheValue = xmlSer.Serialize(msg.ReturnValue);
            //        //Cache the result before return it
            //        var lifespan = cacheAttribute.Lifespan;
            //        if (lifespan.TotalSeconds > 0)
            //        {
            //            //Define the interface to support different type of cache
            //            cache.Set(HashCacheKeyValue, cacheValue, DateTimeOffset.Now.AddMinutes(lifespan.TotalMinutes));
            //        }
            //        else
            //        {
            //            cache.Set(HashCacheKeyValue, cacheValue, DateTimeOffset.Now.AddMinutes(1), null);
            //        }
            //    }

            //    stopwatch.Stop();
            //    Console.WriteLine("***** Time elapsed Executing {0} => {1} *******", ClassMethodName, stopwatch.Elapsed);
            //    return msg;
            //}
        }

        int _Order;
        public int Order
        {
            get
            {
                return _Order;
            }
            set
            {
                _Order = value;
            }
        }
        private static string HashCacheKey(string cacheKey)
        {
            //hash the string as a GUID:
            byte[] hashBytes;
            using (var provider = new MD5CryptoServiceProvider())
            {
                var inputBytes = Encoding.Default.GetBytes(cacheKey);
                hashBytes = provider.ComputeHash(inputBytes);
            }
            return new Guid(hashBytes).ToString();
        }
        private string GetParameterInfo(IMethodInvocation input)
        {
            var str = "";
            for (int i = 0; i < input.Arguments.Count; ++i)
            {
                var paramType = input.Arguments.GetParameterInfo(i).ParameterType.Name.ToLower();
                str += input.Arguments.GetParameterInfo(i).Name + " - " + input.Arguments[i] + " | ";
            }
            return str;
        }

        private static CacheAttribute GetCacheSettings(IMethodInvocation input)
        {
            //TODO : Use injected cacher 
            throw new NotImplementedException();

            ////get the cache attribute & check if overridden in config:
            //var attributes = input.MethodBase.GetCustomAttributes(typeof(CacheAttribute), false);

            //var temp = input.MethodBase.GetCustomAttributes(false);
            //var temp1 = input.MethodBase.GetCustomAttributes(true);

            //var cacheAttribute = (CacheAttribute)attributes[0];
            //if (cacheAttribute.SerializationFormat == SerializationFormat.Null)
            //{
            //    //TODO=>Get Default from Config CacheConfiguration.Current.DefaultSerializationFormat;
            //    cacheAttribute.SerializationFormat = SerializationFormat.Xml;
            //}

            //if (cacheAttribute.CacheType == CacheType.Null)
            //{
            //    //TODO=>Get Default from Config CacheConfiguration.Current.CacheType;
            //    cacheAttribute.CacheType = CacheType.Memory;
            //}
            //return cacheAttribute;
        }
    }
}
