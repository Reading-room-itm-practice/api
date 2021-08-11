using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Core.Response;
using Storage.Interfaces;
using Storage.Models;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ReadStatusUpdaterService : IReadStatusUpdaterService
    {
        private readonly IBaseRepository<ReadStatus> _readStatusRepository;
        private readonly IBaseRepository<Book> _bookRepository;
        private readonly ICreatorService<ReadStatus> _creatorService;
        private readonly IMapper _mapper;
        private readonly ILoggedUserProvider _loggedUser;
        public ReadStatusUpdaterService(
            IBaseRepository<ReadStatus> readStatusRepository,
            IMapper mapper,
            ILoggedUserProvider loggedUser, 
            IBaseRepository<Book> bookRepository,
            ICreatorService<ReadStatus> creatorService)
        {
            _readStatusRepository = readStatusRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
            _loggedUser = loggedUser;
            _creatorService = creatorService;
        }

        public async Task<ServiceResponse> UpdateReadStatus(ReadStatusRequest request)
        {
            var currentUserId = _loggedUser.GetUserId();
            var books = await _bookRepository.FindByConditions(b => b.Id == request.BookId);
            if (!books.Any())
            {
                return ServiceResponse.Error("Book with given id does not exist.");
            }

            var readStatus = await _readStatusRepository.FindByConditions(r => r.BookId == request.BookId && r.CreatorId == currentUserId);
            if (!readStatus.Any())
            {
                await _creatorService.Create<ReadStatusDto>(request);
                return ServiceResponse.Success("Resource has been created.", HttpStatusCode.Created);
            }

            if (!request.IsFavorite && !request.IsRead && !request.IsWantRead)
            {
                await _readStatusRepository.Delete(readStatus.FirstOrDefault());
                return ServiceResponse.Success("Resource has been removed.");
            }

            var updateModel = _mapper.Map(request, readStatus.FirstOrDefault());
            await _readStatusRepository.Edit(updateModel);

            return ServiceResponse.Success("Resource has been updated.");
        }
    }
}
