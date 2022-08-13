using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s
{
    public class OrderDetailDto : IDto
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public string Image{get; set;}
        public string Color { get; set; }
        public string Size { get; set; }
        public int Quantitiy { get; set; }
        public int Price { get; set; }

    }
}
