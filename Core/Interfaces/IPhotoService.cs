using Core.ServiceResponses;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPhotoService
    {
        public Task<ServiceResponse> UploadPhoto(IFormFile image, int bookId);
        public Task<ServiceResponse> DeletePhoto(int id);
    }
}
