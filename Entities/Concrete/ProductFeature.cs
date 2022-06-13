using Core.Entities;

namespace Entities.Concrete
{
    public class ProductFeature : IEntitiy
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int Stock { get; set; }
        public int ImageId { get; set; }
    }
}
