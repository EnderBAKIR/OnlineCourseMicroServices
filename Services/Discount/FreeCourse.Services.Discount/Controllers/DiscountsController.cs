using FreeCourse.Services.Discount.Services;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : CustomBaseController
    {
        private readonly IDiscountServices _discountServices;

        private readonly ISharedIdentityService _sharedIdentityService;

        public DiscountsController(IDiscountServices discountServices, ISharedIdentityService sharedIdentityService)
        {
            _discountServices = discountServices;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {


            return CreateActionResultInstance(await _discountServices.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var discount = await _discountServices.GetById(id);

            return CreateActionResultInstance(discount);
        }


        [HttpGet]
        [Route("/api/[controller]/[action]/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var userId = _sharedIdentityService.GetUserId;

            var discount = await _discountServices.GetByCodeAndUserId(code , userId);

            return CreateActionResultInstance(discount);

        }

        [HttpPost]
        public async Task<IActionResult> Save(Models.Discount discount)
        {

           return CreateActionResultInstance(await _discountServices.Save(discount));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Models.Discount discount)
        {

            return CreateActionResultInstance(await _discountServices.Update(discount));

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            return CreateActionResultInstance(await _discountServices.Delete(id));
        }

    }
}
