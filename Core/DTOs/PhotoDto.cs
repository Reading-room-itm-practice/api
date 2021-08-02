using Core.Common;
using Storage.Models.Photos;
using System.Net.Http;

namespace Core.DTOs
{
    public class PhotoDto : IDto
    {
        public int Id { get; set; }
        public string TypeId { get; set; }
        public PhotoTypes PhotoType { get; set; }
        //public string Path { get; set; }
        public StreamContent Photo { get; set; }
    }
}
