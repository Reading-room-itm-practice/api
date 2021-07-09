using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPhotoService
    {
        public Task<KeyValuePair<int, string>> UploadPhoto(IFormFile image, int bookId);
        public Task<KeyValuePair<int, string>> DeletePhoto(int id);
    }
}
