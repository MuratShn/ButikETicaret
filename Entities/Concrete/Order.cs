using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Order : IEntitiy
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public int SoldProductsId { get; set; }
        public int Fee { get; set; } //toplam fiyat
        public DateTime OrderDate { get; set; }

    }
}
