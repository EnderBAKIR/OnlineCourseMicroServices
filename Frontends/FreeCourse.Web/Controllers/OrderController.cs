using FreeCourse.Web.Models.Orders;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBasketService _baskerService;
        private readonly IOrderService _orderService;

        public OrderController(IBasketService baskerService, IOrderService orderService)
        {
            _baskerService = baskerService;
            _orderService = orderService;
        }

        public async Task< IActionResult> Checkout()
        {
            var basket = await _baskerService.Get();

            ViewBag.basket = basket;
            return View(new CheckOutInfoInput());
        }
    }
}
