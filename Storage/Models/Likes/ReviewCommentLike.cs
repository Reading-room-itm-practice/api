namespace Storage.Models.Likes
{
    public class ReviewCommentLike : Like
    {
        public int ReviewCommentId { get; set; }
        public ReviewComment ReviewComment { get; set; }
    }
}
