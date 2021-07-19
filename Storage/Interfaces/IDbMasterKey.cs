using System.ComponentModel.DataAnnotations;

namespace Storage.Iterfaces
{
    public interface IDbMasterKey
    {
        [Key]
        public int Id { get; set; }
    }
}
