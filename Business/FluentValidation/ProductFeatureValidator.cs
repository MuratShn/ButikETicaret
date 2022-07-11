using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.FluentValidation
{
    public class ProductFeatureValidator : AbstractValidator<ProductFeature>
    {
        public ProductFeatureValidator()
        {
            RuleFor(x=>x.Stock).GreaterThan(0).WithMessage("Stok adedi 0'dan büyük olmak zorunda");
            RuleFor(x => x.Size).Must(SizeCheck).WithMessage("Bedenler Yanlış");
            
            RuleFor(x => x.Color).NotEmpty().WithMessage("Boş olamaz");
            RuleFor(x => x.Size).NotEmpty().WithMessage("Boş olamaz"); 
            RuleFor(x => x.Stock).NotEmpty().WithMessage("Boş olamaz");

        }

        private bool SizeCheck(string arg)
        {
            if(!(arg == "Small" || arg == "Medium" || arg == "Large" || arg == "XLarge"))
            {
                return false;
            }
            return true;
        }
    }
}
