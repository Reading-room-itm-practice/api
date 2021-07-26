namespace Storage.Models.Likes
{
    public class  ReviewLike : Like
    {
        public int ReviewId { get; set; }
        public Review Review { get; set; }
    }
}
