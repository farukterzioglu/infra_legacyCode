using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validation
{
    public abstract class ValidationBase
    {
        private readonly List<string> _validationMessages;
        public List<string> validationMessages { get { return _validationMessages; } }

        private readonly bool _isvalid;
        public bool isvalid { get { return _isvalid; }  }
        
        //private readonly ValidationType _validationType;
        //public ValidationType validationType { get { return _validationType; } }

        public ValidationBase()//ValidationType validationType)
        {
            _validationMessages = new List<string>();
            //_validationType = validationType;
        }
    }

    public abstract class ModelValidationBase : ValidationBase
    {
        private readonly IEntity _entity;
        public IEntity entity;

        public ModelValidationBase(IEntity entity)//, ValidationType validationType) : base(validationType)
        {
            _entity = entity;
        }

        protected abstract void InsertValidation();
        protected abstract void UpdateValidation();
        protected abstract void AddValidation();
        protected abstract void DeleteValidation();
    }
}
