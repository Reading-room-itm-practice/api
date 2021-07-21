using Core.Common;

namespace Core.DTOs
{
    public class UserSearchDto : IDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}
