using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.ValidationResult
{
    public class ValidationResult<T> : IValidationResult<T>
    {
        public bool success { get; }

        public T message { get; }
        public ValidationResult(T Messages,bool Success)
        {
            message = Messages;
            success = Success;
        }
    }
}
