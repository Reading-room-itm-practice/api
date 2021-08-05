using AutoMapper;
using Core.Interfaces;
using Core.ServiceResponses;
using Storage.Identity;
using Storage.Models;
using Storage.Models.Follows;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Interfaces.Follows;

namespace Core.Services.Follows
{
    public class FollowersGetter : IFollowersGetter
    {
        private readonly IExtendedBaseRepository<AuthorFollow> _authorFollowRepo;
        private readonly IExtendedBaseRepository<UserFollow> _userFollowRepo;
        private readonly IExtendedBaseRepository<CategoryFollow> _categoryFollowRepo;
        private readonly IMapper _mapper;

        public FollowersGetter(IExtendedBaseRepository<AuthorFollow> authorFollowRepo,
            IExtendedBaseRepository<UserFollow> userFollowRepo,
            IExtendedBaseRepository<CategoryFollow> categoryFollowRepo, IMapper mapper)
        {
            _authorFollowRepo = authorFollowRepo;
            _userFollowRepo = userFollowRepo;
            _categoryFollowRepo = categoryFollowRepo;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<IDto>>> GetUserFollowers<IDto>(Guid followableId)
        {
            var models = await _userFollowRepo.FindByConditionsWithIncludes(
                x => x.UserId == followableId, nameof(UserFollow.Creator), $"{nameof(UserFollow.Creator)}.{nameof(UserFollow.Creator.ProfilePhoto)}");
            var responseModels = _mapper.Map<IEnumerable<IDto>>(models);

            return ServiceResponse<IEnumerable<IDto>>.Success(responseModels, "Retrieved list with user followers");
        }

        public async Task<ServiceResponse<IEnumerable<IDto>>> GetAuthorFollowers<IDto>(int followableId)
        {
         
            var models = await _authorFollowRepo.FindByConditionsWithIncludes(
                x => x.AuthorId == followableId, nameof(AuthorFollow.Creator), $"{nameof(AuthorFollow.Creator)}.{nameof(AuthorFollow.Creator.ProfilePhoto)}");
            var responseModels = _mapper.Map<IEnumerable<IDto>>(models);

            return ServiceResponse<IEnumerable<IDto>>.Success(responseModels, "Retrieved list with author followers");
        }

        public async Task<ServiceResponse<IEnumerable<IDto>>> GetCategoryFollowers<IDto>(int followableId)
        {
            var models = await _categoryFollowRepo.FindByConditionsWithIncludes(
                x => x.CategoryId == followableId, nameof(CategoryFollow.Creator), $"{nameof(CategoryFollow.Creator)}.{nameof(CategoryFollow.Creator.ProfilePhoto)}");
            var responseModels = _mapper.Map<IEnumerable<IDto>>(models);

            return ServiceResponse<IEnumerable<IDto>>.Success(responseModels, "Retrieved list with category followers");
        }

    }
}
