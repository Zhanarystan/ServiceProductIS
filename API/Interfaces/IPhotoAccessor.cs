using System.Threading.Tasks;
using API.Photos;
using Microsoft.AspNetCore.Http;

namespace API.Interfaces
{
    public interface IPhotoAccessor
    {
        Task<PhotoUploadResult> AddPhoto(IFormFile file);
        Task<string> DeletePhoto(string publicId);

    }
}