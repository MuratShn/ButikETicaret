﻿using Core.Entities;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s
{
    public class ProductDetailDto : IDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string Mold { get; set; }
        public string Material { get; set; }
        public int Stok { get; set; }
        public char Gender { get; set; }
        public bool Status { get; set; }
        public List<string> Colors { get; set; }
        public List<ProductFeature> Features { get; set; }
        public List<ImageDto> Image { get; set; }
        public int Price { get; set; }
    }
}
