using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel_s
{
    public class ProductImageVM : IVM
    {
        public int Id { get; set; }
        public string? ProductPath { get; set; }
        public int ProductId { get; set; }
        public string Color { get; set; }
        public IFormFileCollection Image { get; set; }
    }
}
