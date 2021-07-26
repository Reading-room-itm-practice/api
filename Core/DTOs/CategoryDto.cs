using Core.Common;
using Core.Interfaces;

namespace Core.DTOs
{
    public class CategoryDto : IDto, INameSortable
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
