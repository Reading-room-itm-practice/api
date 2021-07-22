using Storage.Identity;
using System;

namespace Storage.Models
{
    public abstract class AuditableModel
    {
        public DateTime CreatedAt { get; set; }
        public Guid CreatorId { get; set; }
        public User Creator { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public Guid? UpdaterId { get; set; }
        public virtual User Updater { get; set; }
        public bool IsDeleted { get; set; }
    }
}
