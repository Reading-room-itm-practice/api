using Storage.Identity;

namespace Storage.Models.Follows
{
    public class AuthorFollow : Follow
    {
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
