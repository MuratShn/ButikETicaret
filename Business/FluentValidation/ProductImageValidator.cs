using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.FluentValidation
{
    public class ProductImageValidator : AbstractValidator<ProductImage>
    {
        public ProductImageValidator()
        {
            //RuleFor(x => x.Size).Must(SizeCheck).WithMessage("Bedenler Yanlış");
            RuleFor(x => x.Color).NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty().GreaterThan(0).WithMessage("Sistemsel hata");
        }
    }
}