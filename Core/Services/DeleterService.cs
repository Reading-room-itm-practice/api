using Core.Exceptions;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Storage.Interfaces;
using Storage.Iterfaces;
using Storage.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class DeleterService<T> : AuthorizationBasedService, IDeleterService<T> where T : AuditableModel, IDbMasterKey
    {
        private readonly IBaseRepository<T> _repository;

        public DeleterService(IBaseRepository<T> repository, IAuthorizationService authService, ILoggedUserProvider loggedUserProvider) : base(authService, loggedUserProvider)
        {
            _repository = repository;
        }

        public async Task Delete(int id)
        {
            var model = await _repository.FindByConditions(x => x.Id == id);
            await CheckCanBeModify(model.FirstOrDefault());
            await _repository.Delete(model.FirstOrDefault());
        }
    }
}
