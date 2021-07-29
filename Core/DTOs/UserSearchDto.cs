using Core.Common;
using System;

namespace Core.DTOs
{
    public class UserSearchDto : IDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
    }
}
