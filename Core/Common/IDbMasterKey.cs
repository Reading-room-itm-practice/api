using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Common
{
     public interface IDbMasterKey
    {
        [Key]
        public int Id { get; set; }
    }
}
