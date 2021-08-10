using AutoMapper;
using AutoMapper.Internal;
using Core.DTOs;
using Core.Interfaces;
using Storage.Interfaces;
using Storage.Models;
using Storage.Models.Likes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{
    public class LikeableMapperHelper<TLike, TLikeable, TDto> : ILikeableMapperHelper<TLike, TLikeable, TDto> where TLike : Like where TLikeable : AuditableModel, ILikeable<TLike> where TDto: LikeableDto
    {
        private readonly IMapper _mapper;
        private readonly ILoggedUserProvider _loggedUserProvider;

        public LikeableMapperHelper(IMapper mapper, ILoggedUserProvider loggedUserProvider)
        {
            _mapper = mapper;
            _loggedUserProvider = loggedUserProvider;
        }

        public IEnumerable<TDto> Map(IEnumerable<TLikeable> models)
        {

            Guid? userId = null;

            try
            {
                userId =_loggedUserProvider.GetUserId();
            }
            catch (UnauthorizedAccessException)
            {
            }

            return _mapper.Map<IEnumerable<TLikeable>, IEnumerable<TDto>>(models, opt
                => opt.AfterMap((src, dest)
                    => dest.ForAll(x =>
                        x.IsLoggedUserLike = src.Any(x => x.CreatorId == userId))));
        }

        public TDto Map(TLikeable model)
        {
            var predicate = false;
            
            try
            {
                predicate = model.CreatorId == _loggedUserProvider.GetUserId();
            }
            catch (UnauthorizedAccessException) { }

            return _mapper.Map<TLikeable, TDto>(model, opt
                => opt.AfterMap((src, dest)
                    => dest.IsLoggedUserLike = predicate));
        }
    }
}
