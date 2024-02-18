﻿using FreeCourse.Web.Models.Catalog;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<List<CourseViewModel>> GetAllCourseAsync();

        Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId);

        Task<CourseViewModel> GetByCourseIdAsync(string courseId);



        Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput);

        Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput);

        Task<bool> DeleteCourseAsync(string courseId);
    }
}
