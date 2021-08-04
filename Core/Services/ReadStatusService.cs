using AutoMapper;
using Core.Common;
using Core.DTOs;
using Core.Interfaces;
using Core.Repositories;
using Core.Requests;
using Core.ServiceResponses;
using Storage.Interfaces;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ReadStatusService : IReadStatusService
    {
        private readonly IBaseRepository<ReadStatus> _readStatusRepository;
        private readonly IBaseRepository<Book> _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILoggedUserProvider _loggedUser;
        public ReadStatusService(IBaseRepository<ReadStatus> readStatusRepository, IMapper mapper, ILoggedUserProvider loggedUser, IBaseRepository<Book> bookRepository)
        {
            _readStatusRepository = readStatusRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
            _loggedUser = loggedUser;
        }

        public async Task Create(ReadStatusRequest request, int bookId, IEnumerable<ReadStatus> readStatus)
        {
            var createModel = _mapper.Map(request, readStatus.FirstOrDefault());

            createModel.BookId = bookId;
            await _readStatusRepository.Create(createModel);
        }

        public async Task<ServiceResponse> Update(ReadStatusRequest request, int bookId)
        {
            var currentUserId = _loggedUser.GetUserId();
            var books = await _bookRepository.FindByConditions(b => b.Id == bookId);
            if (!books.Any())
            {
                return ServiceResponse.Error("Book with given id does not exist.");
            }

            var readStatus = await _readStatusRepository.FindByConditions(r => r.BookId == bookId && r.CreatorId == currentUserId);
            if (!readStatus.Any())
            {
                await Create(request, bookId, readStatus);
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

        public async Task<ServiceResponse<ReadStatusDto>> GetReadStatus(int bookId)
        {
            var currentUserId = _loggedUser.GetUserId();
            var model = await _readStatusRepository.FindByConditions(r => r.BookId == bookId && r.CreatorId == currentUserId);
            if(!model.Any())
            {
                return ServiceResponse<ReadStatusDto>.Error(null ,"Read status with given id does not exist.");
            }
            var responseModel = _mapper.Map<ReadStatusDto>(model.FirstOrDefault());

            return ServiceResponse<ReadStatusDto>.Success(responseModel, "Retrived resource");
        }
    }
}
