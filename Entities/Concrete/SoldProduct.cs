using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class SoldProduct : IEntitiy
    {
        public int Id { get; set; }
        public int ProductFeatureId { get; set; }
        public int NumberOfProduct { get; set; }
        public int OrderId { get; set; }

    }
}
