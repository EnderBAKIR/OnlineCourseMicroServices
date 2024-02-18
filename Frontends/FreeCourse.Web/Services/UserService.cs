using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services
{
    public class UserService : IUserService
    {
        public Task<UserViewModel> GetUser()//for can access user properties
        {
            throw new NotImplementedException();
        }
    }
}
