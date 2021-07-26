namespace Storage.Models.Photos
{
    public class AuthorPhoto : Photo
    {
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
