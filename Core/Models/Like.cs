using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Core.Common;

namespace Core.Models
{
    [Table("Likes")]
    public class Like : AuditableModel, IDbModel
    {
        public int LikeableId { get; set; }
        public string LikeableType { get; set; }
    }
}
