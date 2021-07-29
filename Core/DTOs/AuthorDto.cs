using Core.Common;
using Core.Interfaces;

namespace Core.DTOs
{
    public class AuthorDto : IDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
    }
}
