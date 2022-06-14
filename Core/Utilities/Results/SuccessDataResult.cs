using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T Data, string Messages, bool Success) : base(Data, Messages, Success)
        {
        }
        public SuccessDataResult(T Data, string Messages) : base(Data, Messages, true)
        {
        }
        public SuccessDataResult(T Data) : base(Data, true)
        {
        }
    }
}
