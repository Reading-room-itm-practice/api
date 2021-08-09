using Core.ServiceResponses;
using Microsoft.AspNetCore.Http;
using Storage.Models.Photos;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPhotoService
    {
        public Task<ServiceResponse> UploadPhoto(IFormFile image, string id, PhotoTypes type);
        public Task EditPhoto(ProfilePhoto oldImage, IFormFile newImage);
        public Task<ServiceResponse> GetPhoto(int id);
        public Task<ServiceResponse> DeletePhoto(int id);
        public Task<ServiceResponse> GetPhotos(string typeId, PhotoTypes type);
    }
}
