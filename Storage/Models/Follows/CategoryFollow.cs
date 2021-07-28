using Storage.Interfaces;

namespace Storage.Models.Follows
{
    public class CategoryFollow : Follow
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
