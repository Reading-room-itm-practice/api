using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Common
{
    public abstract class AuditableModel
    {
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public int LastModifiedBy { get; set; }
    }
}
