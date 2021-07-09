using Storage.Iterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Models
{
    [Table("Follows")]
    public class Follow : AuditableModel, IDbModel
    {
        public int FollowableId { get; set; }
        public string FollowableType { get; set; }
    }
}
