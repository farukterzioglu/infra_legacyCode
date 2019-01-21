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
    public class LocalizedValidationAttribute : ValidationAttribute
    {
        public LocalizedValidationAttribute()
        {
            throw
                new NotImplementedException();
        }
        //TODO : get localized validation message
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
