using Storage.Models;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IModifyAvalibilityChecker
    {
        public Task CheckCanBeModify(AuditableModel? model);
    }
}
