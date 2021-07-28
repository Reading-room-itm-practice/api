using System.ComponentModel.DataAnnotations;

namespace Storage.Interfaces
{
    public interface IDbMasterKey : IDbModel
    {
        [Key]
        public int Id { get; set; }
    }
}
