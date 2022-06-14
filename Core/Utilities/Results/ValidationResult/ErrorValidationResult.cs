using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.ValidationResult
{
    public class ErrorValidationResult<T> : ValidationResults<T>
    {
        public ErrorValidationResult(T Error, bool Succes) : base(Error, Succes)
        {
        }
        public ErrorValidationResult(T Error) : base(Error, false)
        {
        }
    }
}
