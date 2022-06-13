using Core.Entities;

namespace Entities.Concrete
{
    public class Product : IEntitiy
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string Mold{ get; set; }
        public string Material{ get; set; }
        public int Stok{ get; set; }
        public bool Gender{ get; set; }
        public bool Status{ get; set; }
        public int Price{ get; set; }
    }
}
