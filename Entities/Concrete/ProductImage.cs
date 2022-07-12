using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ProductImage : IEntitiy
    {
        public int Id { get; set; }
        public string ProductPath { get; set; }
        public int ProductId { get; set; }
        public string Color { get; set; }
    }
}
