using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s
{
    public class CartDto
    {
        public int ProductId { get; set; }
        public int FeaturesId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public ImageDto Image{ get; set; }
    }
}
