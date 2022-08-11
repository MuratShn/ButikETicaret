using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s
{
    public class BasketDto : IDto
    {
        public ProductFeature Feature { get; set; }
        public int Quantitiy { get; set; }
    }
}
