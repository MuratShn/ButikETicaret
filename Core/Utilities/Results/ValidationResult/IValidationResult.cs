using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.ValidationResult
{
    public interface IValidationResult<T> : IResult
    {
        bool success { get; }
        T message { get; }
    }
}
