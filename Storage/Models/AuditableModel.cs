using Storage.Identity;
using System;

namespace Storage.Models
{
    public abstract class AuditableModel 
    {
        public DateTime Created { get; set; }
        public Guid CreatorId { get; set; }
        public User User { get; set; }
        public DateTime? LastModified { get; set; }
        public Guid? UpdaterId{ get; set; }
        public User Updater { get; set; }
    }
}
