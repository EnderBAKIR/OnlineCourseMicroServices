using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Helpers;
using FreeCourse.Web.Models;
using FreeCourse.Web.Models.Catalog;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services
{
 
    public class CatalogService : ICatalogService
    {

        private readonly HttpClient _httpClient;

        private readonly IPhotostockService _photostockService;

        private readonly PhotoHelper _photoHelper;

        public CatalogService(HttpClient httpClient, IPhotostockService photostockService , PhotoHelper photoHelper)
        {
            _httpClient = httpClient;
            _photostockService = photostockService;
            _photoHelper = photoHelper;
        }

        public async Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput)
        {

            var resultPhotoService = await _photostockService.UploadPhoto(courseCreateInput.PhotoFormFile);

            if (resultPhotoService != null)
            {
                courseCreateInput.Picture = resultPhotoService.Url;
            }
           


            var response = await _httpClient.PostAsJsonAsync<CourseCreateInput>("courses", courseCreateInput);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var response = await _httpClient.DeleteAsync($"courses/{courseId}");

            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            var response = await _httpClient.GetAsync("categories");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSucces = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();

            return responseSucces.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseAsync()
        {
            //localhost:5000/services/catalog/courses
            var response = await _httpClient.GetAsync("courses");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSucces = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();



            responseSucces.Data.ForEach(x =>
            {
                x.Picture = _photoHelper.GetPhotoStockUrl(x.Picture);
            });


            return responseSucces.Data;

        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {

            //[controller]/GetAllByUserId/{userId}

            var response = await _httpClient.GetAsync($"courses/GetAllByUserId/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSucces = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            responseSucces.Data.ForEach(x =>
            {
                x.Picture = _photoHelper.GetPhotoStockUrl(x.Picture);
            });

           

            return responseSucces.Data;
        }

        public async Task<CourseViewModel> GetByCourseIdAsync(string courseId)
        {
            var response = await _httpClient.GetAsync($"courses/{courseId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSucces = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();

            return responseSucces.Data;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput)
        {


            var resultPhotoService = await _photostockService.UploadPhoto(courseUpdateInput.PhotoFormFile);

            if (resultPhotoService != null)
            {
                _photostockService.DeletePhoto(courseUpdateInput.Picture);
                courseUpdateInput.Picture = resultPhotoService.Url;
            }

            var response = await _httpClient.PutAsJsonAsync<CourseUpdateInput>("courses", courseUpdateInput);

            return response.IsSuccessStatusCode;
        }
    }
}
