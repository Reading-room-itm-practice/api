using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Core.Response;
using Storage.Interfaces;
using Storage.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ReadStatusGetterService : IReadStatusGetterService
    {
        private readonly IBaseRepository<ReadStatus> _readStatusRepository;
        private readonly IMapper _mapper;
        private readonly ILoggedUserProvider _loggedUser;
        public ReadStatusGetterService(
            IBaseRepository<ReadStatus> readStatusRepository,
            IMapper mapper,
            ILoggedUserProvider loggedUser)
        {
            _readStatusRepository = readStatusRepository;
            _mapper = mapper;
            _loggedUser = loggedUser;
        }

        public async Task<ServiceResponse<ReadStatusDto>> GetReadStatus(int bookId)
        {
            var currentUserId = _loggedUser.GetUserId();
            var model = await _readStatusRepository.FindByConditions(r => r.BookId == bookId && r.CreatorId == currentUserId);
            if (!model.Any())
            {
                return ServiceResponse<ReadStatusDto>.Error(null, "Read status with given id does not exist.");
            }
            var responseModel = _mapper.Map<ReadStatusDto>(model.FirstOrDefault());

            return ServiceResponse<ReadStatusDto>.Success(responseModel, "Retrived resource");
        }
    }
}
