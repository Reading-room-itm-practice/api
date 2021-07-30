using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class SentFriendRequestDto : IDto
    {
        public int Id { get; set; }
        public Guid ToId { get; set; }
        public string UserName { get; set; }
    }
}
