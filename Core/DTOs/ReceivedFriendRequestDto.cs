using Core.Common;
using Storage.Interfaces;
using System;

namespace Core.DTOs
{
    public class ReceivedFriendRequestDto : IDto
    {
        public int Id { get; set; }
        public Guid CreatorId { get; set; }
        public string UserName { get; set; }
    }
}
