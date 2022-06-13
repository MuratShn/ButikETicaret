using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class Result : IGeneralResult
    {
        public bool success { get; }

        public string message { get; }

        public Result(string Message,bool Success)
        {
            success = Success;
            message = Message;
        }
        public Result( bool Success)
        {
            success = Success;
        }
    }
}
