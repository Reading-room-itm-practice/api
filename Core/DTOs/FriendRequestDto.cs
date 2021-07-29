using Core.Common;
using Storage.Interfaces;
using System;

namespace Core.DTOs
{
    public class FriendRequestDto : IDto//, IApproveable
    {
        public int Id { get; set; }
        public Guid CreatorId { get; set; }
        //public Guid ToId { get; set; }
        //public bool Approved { get; set; }
    }
}
