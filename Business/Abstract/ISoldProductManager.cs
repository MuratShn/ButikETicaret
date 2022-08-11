using Core.Utilities.Results;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISoldProductManager
    {
        IResult Add(List<BasketDto> entity,int orderId);
    }
}
