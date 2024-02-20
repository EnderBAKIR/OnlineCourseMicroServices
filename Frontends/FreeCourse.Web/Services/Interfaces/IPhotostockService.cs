using FreeCourse.Web.Models.PhotoStocks;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IPhotostockService
    {

        Task<PhotoViewModel> UploadPhoto(IFormFile photo);

        Task<Boolean> DeletePhoto(string photoUrl);

    }
}
