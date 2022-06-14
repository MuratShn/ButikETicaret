using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.ValidationResult
{
    public class SuccesValidationResult<T> : ValidationResults<T>
    {
        public SuccesValidationResult(T Error, bool Succes) : base(Error, Succes)
        {
        }
        public SuccesValidationResult(T Error) : base(Error, true)
        {
        }
    }
}
