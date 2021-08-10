using Core.DTOs;
using Storage.Interfaces;
using Storage.Models.Likes;
using System.Collections.Generic;
using Storage.Models;

namespace Core.Interfaces
{
    public interface ILikeableMapperHelper<TLike, in TLikeable, out TDto> where TLike : Like where TLikeable : AuditableModel, ILikeable<TLike> where TDto : LikeableDto
    {
        public IEnumerable<TDto> Map(IEnumerable<TLikeable> models);
        public TDto Map(TLikeable model);
    }
}
