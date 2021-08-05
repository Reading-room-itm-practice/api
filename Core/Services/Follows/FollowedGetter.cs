using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.Interfaces;
using Core.Interfaces.Follows;
using Core.ServiceResponses;
using Storage.Identity;
using Storage.Models;
using Storage.Models.Follows;

namespace Core.Services.Follows
{
    public sealed class FollowedGetter<T> : IFollowedGetter<T> where T : Follow
    {
        private readonly IExtendedBaseRepository<T> _followsRepository;
        private readonly IMapper _mapper;

        public FollowedGetter(IExtendedBaseRepository<T> followsRepository, IMapper mapper)
        {
            _followsRepository = followsRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<IDto>>> GetFollowed<IDto>(Guid followerId)
        {
            var models = await _followsRepository.FindByConditionsWithIncludes(x => x.CreatorId == followerId, GetIncludes());
            var responseModels = _mapper.Map<IEnumerable<IDto>>(models);

            return ServiceResponse<IEnumerable<IDto>>.Success(responseModels, "Retrieved list with resources by creator id");
        }

        private static string[] GetIncludes()
        {
            if (typeof(T) == typeof(AuthorFollow))
            {
                return new[] { nameof(Author), $"{nameof(Author)}.{nameof(Author.MainPhoto)}" };
            }
            if (typeof(T) == typeof(CategoryFollow))
            {
                return new[] {nameof(Category) };
            }
            if (typeof(T) == typeof(UserFollow))
            {
                return new[] { nameof(User), $"{nameof(User)}.{nameof(User.ProfilePhoto)}" };
            }

            return Array.Empty<string>();
        }
    }
}
