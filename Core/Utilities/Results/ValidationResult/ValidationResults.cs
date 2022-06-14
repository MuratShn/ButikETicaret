using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.ValidationResult
{
    public class ValidationResults<T> : IValidationResults<T>
    {
        public T messages { get; }

        public bool success { get; }

        public ValidationResults(T Messages,bool Succes)
        {
            messages = Messages;
            success = Succes;
        }
    }
}
