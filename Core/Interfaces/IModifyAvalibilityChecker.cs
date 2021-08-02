using Storage.Models;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IModifyAvalibilityChecker
    {
#nullable enable
        public Task CheckCanBeModify(AuditableModel? model);
    }
}
