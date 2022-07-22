using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s
{
    public class ImageDto : IDto
    {
        public string Color { get; set; }
        public string Image { get; set; }

    }
}
