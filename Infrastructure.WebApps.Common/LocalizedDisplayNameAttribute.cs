using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;

namespace Infrastructure.WebApps.Common
{
    public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        private readonly string m_ResourceName;
        private readonly string m_ClassName;
        public LocalizedDisplayNameAttribute(string className, string resourceName)
        {
            m_ResourceName = resourceName;
            m_ClassName = className;
        }

        public override string DisplayName
        {
            get
            {
                if (m_ResourceName == null || m_ClassName == null) return null;

                // get and return the resource object
                var globalResourceObject = HttpContext.GetGlobalResourceObject(
                    m_ClassName,
                    m_ResourceName,
                    Thread.CurrentThread.CurrentCulture);
                return globalResourceObject != null ? globalResourceObject.ToString() : "No Label";
            }
        }

    }
}
