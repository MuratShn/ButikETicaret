using Core.Entities;

namespace Entities.Concrete
{
    public class Product : IEntitiy
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public int? UserId { get; set; }
        public string Mold{ get; set; }
        public string Material{ get; set; }
        public int Stok{ get; set; }
        public char Gender{ get; set; }
        //0 = Not known; Bilinmiyor
        //1 = Male; Erkek
        //2 = Female; Kadın 
        //9 = Not applicable => Unisex
        public bool Status{ get; set; }
        public int Price{ get; set; }
    }
}
