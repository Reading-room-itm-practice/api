using Storage.Identity;
using Storage.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    [Table("Friend_requests")]
    public class FriendRequest : AuditableModel, IDbMasterKey<int>
    {
        [Key]
        public int Id { get; set; }
        public Guid ToId { get; set; }
        public User To { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
