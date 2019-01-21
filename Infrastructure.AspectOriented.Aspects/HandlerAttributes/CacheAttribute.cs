using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Infrastructure.CrossCutting.Cache;
using Infrastructure.Domain.CrossCutting.Cache;

namespace Infrastructure.AspectOriented.Aspects.HandlerAttributes
{
    public class CacheAttribute : HandlerAttribute
    {
        public CacheAttribute(int order)
        {
            base.Order = order;
        }

        /// <summary>
        /// Lifespan of the response in the cache
        /// </summary>
        public TimeSpan Lifespan
        {
            get { return new TimeSpan(Days, Hours, Minutes, Seconds); }
        }
        /// <summary>
        /// Whether caching is enabled
        /// </summary>
        public bool Disabled { get; set; }
        /// <summary>
        /// Days the element to be cached should live in the cache
        /// </summary>
        public int Days { get; set; }
        /// <summary>
        /// Hours the element to be cached should live in the cache
        /// </summary>
        public int Hours { get; set; }
        /// <summary>
        /// Minutes the element to be cached should live in the cache
        /// </summary>
        public int Minutes { get; set; }
        /// <summary>
        /// Seconds the items should live in the cache
        /// </summary>
        public int Seconds { get; set; }
        /// <summary>
        /// The type of cache required for the item
        /// </summary>
        public CacheType CacheType { get; set; }
        /// <summary>
        /// The type of serialization used for the cache key and cached item
        /// </summary>
        public CrossCutting.Cache.SerializationFormat SerializationFormat { get; set; }
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new Handlers.CachingBehavior();
        }
    }
}
