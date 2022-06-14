using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.ValidationResult
{
    public class ErrorValidationResult<T> : ValidationResults<T>
    {
        public ErrorValidationResult(T Messages, bool Succes) : base(Messages, Succes)
        {
        }
        public ErrorValidationResult(T Messages) : base(Messages, false)
        {
        }
    }
}
