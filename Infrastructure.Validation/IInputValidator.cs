//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using MyTrips.Bussiness.Models.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace MyTrips.Bussiness.Abstract
//{

//    #region Validation Sample

//    #region Validation Abstract
//    public class ValidationErrorBackEnd
//    {
//        public string Message;
//    }

//    public interface IInputValidator
//    {
//        List<ValidationErrorBackEnd> Validate<T>(T input);
//    }

//    public class ValidateInputAttribute : ActionFilterAttribute
//    {
//        private readonly IValidatorService _validatorService;
//        private List<IInputValidator> _validators;

//        private readonly Type _type;
//        private readonly string _parameterName = String.Empty;

//        public ValidateInputAttribute(string parameterName)
//        {
//            _parameterName = parameterName;

//            //TODO : Inject this 
//            _validatorService = new StaticValidatorService(); //new DbValidatorService()
//        }

//        public ValidateInputAttribute(string parameterName, params IInputValidator[] validators)
//        {
//            _parameterName = parameterName;

//            this._validators = validators.ToList();
//        }

//        public override void OnActionExecuting(ActionExecutingContext context)
//        {
//            Type type;
//            if (!string.IsNullOrEmpty(_parameterName))
//            {
//                var argument = context.ActionArguments.FirstOrDefault(x => x.Key == _parameterName);
//                if (argument.Value != null)
//                    type = context.ActionArguments.First(x => x.Key == _parameterName).Value.GetType();
//                else return;
//            }
//            else
//                type = _type;

//            _validators = _validatorService.GetValidators(type);
//            var value = context.ActionArguments.First(x => x.Key == _parameterName).Value;

//            List<ValidationErrorBackEnd> errorList = new List<ValidationErrorBackEnd>();

//            _validators.ForEach(x =>
//            {
//                var errors = x.Validate(value);
//                if (errors != null)
//                    errorList.AddRange(errors);
//            });

//            var controller = context.Controller as Controller;

//            errorList.ForEach(x => controller.ModelState.TryAddModelError(_parameterName, x.Message));
//        }
//    }

//    #endregion

//    #region Validators

//    public class InfantListValidator : IInputValidator
//    {
//        List<ValidationErrorBackEnd> IInputValidator.Validate<T>(T input)
//        {
//            List<InfantViewModel> inputCasted = input as List<InfantViewModel>;

//            List<ValidationErrorBackEnd> list = new List<ValidationErrorBackEnd>();
//            if (inputCasted == null)
//                list.Add(new ValidationErrorBackEnd() { Message = "InfantListEmpty" });
//            else
//            {
//                if (inputCasted.Count < 2)
//                    list.Add(new ValidationErrorBackEnd() { Message = "AddMoreInfant" });
//            }

//            return list;
//        }
//    }

//    public class ListValidator : IInputValidator
//    {
//        List<ValidationErrorBackEnd> IInputValidator.Validate<T>(T input)
//        {
//            return null;

//            List<object> inputCasted = input as List<object>;

//            if (inputCasted == null || inputCasted.Count == 0)
//                return new List<ValidationErrorBackEnd>() { new ValidationErrorBackEnd() { Message = "" } };

//            return null;
//        }
//    }

//    public class TcNumberValidator : IInputValidator
//    {
//        List<ValidationErrorBackEnd> IInputValidator.Validate<T>(T input)
//        {
//            string inputCasted = input as string;

//            if (inputCasted.Length < 11)
//                return new List<ValidationErrorBackEnd>() { new ValidationErrorBackEnd() { Message = "" } };

//            return null;
//        }
//    }

//    #endregion

//    #region Validator Service
//    public interface IValidatorService
//    {
//        List<IInputValidator> GetValidators(Type type);
//    }

//    public abstract class ValidatorService : IValidatorService
//    {
//        private Dictionary<Type, List<IInputValidator>> _list;

//        protected abstract Dictionary<Type, List<IInputValidator>> PopulateValidators();

//        public List<IInputValidator> GetValidators(Type type)
//        {
//            _list = this.PopulateValidators();

//            if (_list.ContainsKey(type)) return _list[type].ToList();
//            else return null;
//        }
//    }

//    public class StaticValidatorService : ValidatorService
//    {
//        private readonly static Dictionary<Type, List<IInputValidator>> _list;
//        static StaticValidatorService()
//        {
//            _list = new Dictionary<Type, List<IInputValidator>>();

//            _list.Add(typeof(List<InfantViewModel>), new List<IInputValidator>() {
//                new ListValidator(),
//                new InfantListValidator(),
//            });

//            _list.Add(typeof(string), new List<IInputValidator>() {
//                new TcNumberValidator()
//            });
//        }

//        protected override Dictionary<Type, List<IInputValidator>> PopulateValidators()
//        {
//            return _list;
//        }
//    }

//    //Sample
//    public class DbValidatorService : ValidatorService
//    {
//        protected override Dictionary<Type, List<IInputValidator>> PopulateValidators()
//        {
//            Dictionary<Type, List<IInputValidator>> _list = new Dictionary<Type, List<IInputValidator>>();

//            //TODO : Populate list from db 

//            return _list;
//        }
//    }

//    //Sample
//    public class ReflectionValidatorService : ValidatorService
//    {
//        protected override Dictionary<Type, List<IInputValidator>> PopulateValidators()
//        {
//            Dictionary<Type, List<IInputValidator>> _list = new Dictionary<Type, List<IInputValidator>>();

//            //TODO : Populate list with reflection 

//            return _list;
//        }
//    }

//    #endregion

//    #endregion
//}
