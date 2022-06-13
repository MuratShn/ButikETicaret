using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class DataResult<T> : IDataResult<T>
    {
        public T data { get; }

        public bool success { get; }

        public string messages { get; }
        public DataResult(T Data,string Messages,bool Success)
        {
            data = Data;
            messages = Messages;
            success = Success;
        }
    }
}
