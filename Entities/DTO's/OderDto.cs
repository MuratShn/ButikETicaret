using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s
{
    public class OderDto :IDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<BasketDto> SoldProducts { get; set; }
        public int Fee { get; set; }

    }
}

//id: number
//userId:number
//orderDate:Date
//addressId:number
//soldProducts:CartItem[]
//fee:number