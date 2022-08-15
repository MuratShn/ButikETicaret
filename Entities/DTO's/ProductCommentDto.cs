using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s
{
    public class ProductCommentDto : IDto
    {
        public string FirstName{ get; set; }
        public string SurName{ get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
    }
}
