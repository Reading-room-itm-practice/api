using AutoMapper;
using Core.DTOs.Follows;
using Core.Interfaces;
using Core.Requests.Follows;
using Core.ServiceResponses;
using Storage.Models.Follows;
using System.Net;
using System.Threading.Tasks;

namespace Core.Services
{
    public class FollowsCreatorService<T> : IFollowCreatorService<T> where T : Follow
    {
        private readonly IBaseRepository<T> _repository;
        private readonly IMapper _mapper;

        public FollowsCreatorService(IBaseRepository<T> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> Create(FollowRequest followRequest)
        {
            var model = _mapper.Map<T>(followRequest);
            await _repository.Create(model);
            var responseModel = _mapper.Map<FollowDto>(model);

            return ServiceResponse<FollowDto>.Success(responseModel, "Resorce has been created.", HttpStatusCode.Created);
        }
    }
}
