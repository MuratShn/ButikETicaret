using Core.Entities;

namespace Entities.Concrete
{
    public class LikedProduct : IEntitiy
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}
