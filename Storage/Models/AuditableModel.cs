using System;

namespace Storage.Models
{
    public abstract class AuditableModel
    {
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public int LastModifiedBy { get; set; }
    }
}
