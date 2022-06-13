using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T Data, string Messages, bool Success) : base(Data, Messages, Success)
        {
        }
        public ErrorDataResult(T Data, string Messages) : base(Data, Messages, false)
        {
        }
    }
}
