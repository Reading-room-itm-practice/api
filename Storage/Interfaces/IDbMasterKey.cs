using System.ComponentModel.DataAnnotations;

namespace Storage.Interfaces
{
    public interface IDbMasterKey<T> : IDbModel where T : notnull
    {
        [Key]
        public T Id { get; set; }
    }
}
