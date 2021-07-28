using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Microsoft.EntityFrameworkCore;
using Storage.DataAccessLayer;
using Storage.Models.Photos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class PhotoRepository : BaseRepository<Photo>, IPhotoRepository
    {
        private readonly IMapper _mapper;
        public PhotoRepository(IMapper mapper, ApiDbContext context) : base(context)
        {
            _mapper = mapper;
        }



        public async Task<PhotoDto> UploadPhoto(PhotoUploadRequest photo, PhotoTypes type)
        {
            switch (type)
            {
                case (PhotoTypes.AuthorPhoto):
                    var authorPhoto = _mapper.Map<AuthorPhoto>(photo);
                    return _mapper.Map<PhotoDto>(await Create(authorPhoto));
                case (PhotoTypes.BookPhoto):
                    var bookPhoto = _mapper.Map<BookPhoto>(photo);
                    return _mapper.Map<PhotoDto>(await Create(bookPhoto));
                case (PhotoTypes.ProfilePhoto):
                    var profilePhoto = _mapper.Map<ProfilePhoto>(photo);
                    return _mapper.Map<PhotoDto>(await Create(profilePhoto));
                default:
                    return null;
            }
        }
    }
}
