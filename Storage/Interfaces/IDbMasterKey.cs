using System.ComponentModel.DataAnnotations;

namespace Storage.Iterfaces
{
    public interface IDbMasterKey : IDbModel
    {
        [Key]
        public int Id { get; set; }
    }
}
