using Core.Utilities.Results;
using Core.Utilities.Results.ValidationResult;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.FluentValidation
{
    public static class ValidationTool<T,EntitiyType> where T: IValidator,new () 
    {
        public static IValidationResults<List<string>> Validate(EntitiyType Entitiy)
        {
            var context = new ValidationContext<EntitiyType>(Entitiy);
            var validator = new T();
            var result = validator.Validate(context);
            
            if (!result.IsValid)
            {
                var errors = new List<string>();

                foreach (var item in result.Errors)
                {
                    errors.Add(item.ToString());
                }
                return new ErrorValidationResult<List<string>>(errors, false);
            }
            return new SuccesValidationResult<List<string>>(new List<string>());

        }
    }
}
