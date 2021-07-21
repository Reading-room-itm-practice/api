using Storage.Identity;
using Storage.Iterfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Storage.Models
{
    [Table("Notifications")]
    public class Notification : IDbMasterKey
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Content { get; set; }

    }
}
