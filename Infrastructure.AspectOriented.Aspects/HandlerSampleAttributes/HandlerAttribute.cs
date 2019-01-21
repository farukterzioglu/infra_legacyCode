using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Infrastructure.Aspects.HandlerAttributes
{
    public abstract class HandlerAttribute : Attribute
    {
        /// Derived classes implement this method. When called, it creates a 
        /// new call handler as specified in the attribute configuration.
        /// The parameter "container" specifies the IUnityContainer 
        /// to use when creating handlers, if necessary.
        /// returns a new call handler object.
        public abstract ICallHandler CreateHandler(IUnityContainer container);

        private int order;
        /// <summary>
        /// Gets or sets the order in which the handler will be executed.
        /// </summary>
        public int Order
        {
            get { return this.order; }
            set { this.order = value; }
        }
    }
}
