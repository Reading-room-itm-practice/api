using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Common;

namespace WebAPI.Models
{
    [Table("likes")]
    public class Like : AuditableModel
    {
        public int LikeableId { get; set; }
        public string LikeableType { get; set; }
    }
}
