using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s
{
    public class MyOrderDto 
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderPrice { get; set; }
        public List<OrderDetailDto> OrderDetail { get; set; }
    }
}
