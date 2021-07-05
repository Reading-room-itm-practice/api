using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    [Table("Read_statuses")]
    public class ReadStatus
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public bool IsRead { get; set; }
        public bool IsWantRead { get; set; }
        public bool IsFavorite { get; set; }
    }
}
