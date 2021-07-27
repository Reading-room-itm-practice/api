using AutoMapper;
using Core.Common;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Storage.Interfaces;
using Storage.Iterfaces;
using Storage.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UpdaterService<T> : AuthorizationBasedService, IUpdaterService<T> where T : AuditableModel, IDbMasterKey
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IMapper _mapper;

        public UpdaterService(IMapper mapper, IBaseRepository<T> repository, IAuthorizationService authService, ILoggedUserProvider loggedUserProvider) : base(authService, loggedUserProvider)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Update(IRequest requestDto, int id)
        {
            var model = await _repository.FindByConditions(x => x.Id == id);
            await CheckCanBeModify(model.FirstOrDefault());
            var updatedModel = _mapper.Map(requestDto, model.FirstOrDefault());
            await _repository.Edit(updatedModel);
        }
    }
}
