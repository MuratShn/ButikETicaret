using Core.Entities;

namespace Entities.Concrete
{
    public class Category : IEntitiy
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
