using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BusinessRules
    {
        public static IResult Rules(params IResult[] results)
        {
            foreach (var item in results)
            {
                if (!item.success)
                {
                    return item;
                }
            }
            return new SuccessResult();
        }
    }
}
