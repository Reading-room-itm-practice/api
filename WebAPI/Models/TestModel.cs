using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{

    [Table("test_models")]
    public class TestModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TestValue { get; set; }
    }
}
