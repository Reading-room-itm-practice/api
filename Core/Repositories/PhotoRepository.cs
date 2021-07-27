using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
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

        public async Task UpdatePhoto(int id, PhotoUpdateRequest photo, PhotoTypes type)
        {
            //var editedPhoto = _context.Photos.FirstOrDefault(p => p.Id == id);
            var updatedPhoto = _mapper.Map(photo, _context.Photos.FirstOrDefault(p => p.Id == id));
            
            switch (type)
            {
                case (PhotoTypes.AuthorPhoto):
                    var authorPhoto = _mapper.Map<AuthorPhoto>(updatedPhoto);
                    //authorPhoto.Path = editedPhoto.Path;
                    await Edit(authorPhoto);
                    break;
                case (PhotoTypes.BookPhoto):
                    //var bookPhoto = _mapper.Map<BookPhoto>(updatedPhoto);
                    //bookPhoto.Path = editedPhoto.Path;
                    await Edit(_mapper.Map<BookPhoto>(updatedPhoto));
                    break;
                case (PhotoTypes.ProfilePhoto):
                    var profilePhoto = _mapper.Map<ProfilePhoto>(updatedPhoto);
                    await Edit(profilePhoto);
                    break;
            }
        }
    }
}
