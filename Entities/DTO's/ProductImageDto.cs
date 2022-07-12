using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s
{
    public class ProductImageDto : IDto
    {
        public int Id { get; set; }
        public string ProductPath { get; set; }
        public int ProductId { get; set; }
        public string Color { get; set; }
        public IFormFile Image { get; set; }
    }
}
