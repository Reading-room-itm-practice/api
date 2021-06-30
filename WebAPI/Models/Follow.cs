using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Common;

namespace WebAPI.Models
{
    [Table("follows")]
    public class Follow : AuditableModel
    {
        public int FollowableId { get; set; }
        public string FollowableType { get; set; }
    }
}
