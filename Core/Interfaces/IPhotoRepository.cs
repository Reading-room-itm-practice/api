using Core.DTOs;
using Core.Requests;
using Storage.Models.Photos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPhotoRepository
    {
        public Task<Photo> UploadPhoto(Photo photo);
        public Task<Photo> GetPhoto(int photoId);
        public Task<IEnumerable<AuthorPhoto>> GetAuthorPhotos(int authorId);
        public Task<IEnumerable<BookPhoto>> GetBookPhotos(int bookId);
        public Task<ProfilePhoto> GetUserPhotos(Guid userId);
        public Task<bool> DeletePhoto(int photoId);
    }
}
