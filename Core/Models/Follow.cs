using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Core.Common;

namespace Core.Models
{
    [Table("Follows")]
    public class Follow : AuditableModel, IDbModel
    {
        public int FollowableId { get; set; }
        public string FollowableType { get; set; }
    }
}
