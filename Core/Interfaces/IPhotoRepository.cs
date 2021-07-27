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
        public Task<PhotoDto> UploadPhoto(PhotoUploadRequest photo, PhotoTypes type);
        public Task UpdatePhoto(int id, PhotoUpdateRequest photo, PhotoTypes type);
    }
}
