namespace Storage.Models.Photos
{
    public class BookPhoto : Photo
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
