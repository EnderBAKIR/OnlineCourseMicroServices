using FreeCouse.IdentityServer.Models;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCouse.IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator// Custom implementation of IResourceOwnerPasswordValidator for handling Resource Owner Password Credentials (ROPC) flow
                                                                                         // Validates user credentials (email and password) using UserManager, and provides custom error responses if authentication fails
                                                                                         // Utilizes IdentityServer4 to generate a GrantValidationResult upon successful authentication
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {

            var existUser = await _userManager.FindByEmailAsync(context.UserName);


            if(existUser == null) 
            {
             var errors = new Dictionary<string , object>();
                errors.Add("errors", new List<string> { "Email veya şifreniz yanlış" });

                context.Result.CustomResponse = errors;

                return;
            
            
            
            }
            var passwordCheck= await _userManager.CheckPasswordAsync(existUser,context.Password);


            if (passwordCheck == false)
            {
                var errors = new Dictionary<string, object>();
                errors.Add("errors", new List<string> { "Email veya şifreniz yanlış" });

                context.Result.CustomResponse = errors;

                return;



            }

            context.Result = new GrantValidationResult(existUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);


        }
    }
}
