using Storage.Iterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Models
{
    [Table("Notifications")]
    public class Notification : IDbModel, IDbMasterKey
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }

    }
}
