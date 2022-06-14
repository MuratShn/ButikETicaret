using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.ValidationResult
{
    public class ValidationResults<T> : IValidationResults<T>
    {
        public T errors { get; }

        public bool success { get; }

        public string message { get; }

        public ValidationResults(T Errors,bool Succes,string Message)
        {
            success = Succes;
            errors = Errors;
            message = Message;
        }
        public ValidationResults(T Errors, bool Succes)
        {
            success = Succes;
            errors = Errors;
        }
    }
}
