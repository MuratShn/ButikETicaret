using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.ValidationResult
{
    public class SuccesValidationResult<T> : ValidationResults<T>
    {
        public SuccesValidationResult(T Messages, bool Succes) : base(Messages, Succes)
        {
        }
        public SuccesValidationResult(T Messages) : base(Messages, true)
        {
        }
    }
}
