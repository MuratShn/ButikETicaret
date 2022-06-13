using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Address :IEntitiy
    {
        public int Id { get; set; }
        public string Adress { get; set; }
    }
}
