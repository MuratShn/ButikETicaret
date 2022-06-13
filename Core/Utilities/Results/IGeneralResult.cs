using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public interface IGeneralResult
    {
        public bool success { get; }
        public string message { get; }
    }
}
