using AutoMapper;
using Core.DTOs;
using Core.Interfaces;
using Core.Requests;
using Microsoft.EntityFrameworkCore;
using Storage.DataAccessLayer;
using Storage.Models.Photos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class PhotoRepository : BaseRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(ApiDbContext context) : base(context) { }

        public async Task<Photo> UploadPhoto(Photo photoRequest)
        {
            return await Create(photoRequest);
        }

        public async Task<Photo> GetPhoto(int photoId)
        {
            return (await FindByConditions(p => p.Id == photoId)).FirstOrDefault();
        }

        public IEnumerable<AuthorPhoto> GetAuthorPhotos(int authorId)
        {
            return  _context.AuthorPhotos.Where(p => p.AuthorId == authorId);
        }

        public IEnumerable<BookPhoto> GetBookPhotos(int bookId)
        {
            return _context.BookPhotos.Where(p => p.BookId == bookId);
        }

        public async Task<ProfilePhoto> GetUserPhotos(Guid userId)
        {
            var user = _context.Users.Include(u => u.ProfilePhoto).FirstOrDefault(u => u.Id == userId);
            return await _context.ProfilePhotos.FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<bool> DeletePhoto(int photoId)
        {
            var photoToDelete = GetPhoto(photoId).Result;
            if (photoToDelete == null) return false;
            await Delete(photoToDelete);
            File.Delete(photoToDelete.Path);
            return true;
        }
    }
}
