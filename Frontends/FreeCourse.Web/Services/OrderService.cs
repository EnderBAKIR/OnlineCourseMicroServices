using FreeCourse.Web.Models.Orders;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly IPaymentService _paymentService;
        private readonly IBasketService _basketService;

        public OrderService(HttpClient httpClient, IPaymentService paymentService, IBasketService basketService)
        {
            _httpClient = httpClient;
            _paymentService = paymentService;
            _basketService = basketService;
        }

        public Task<OrderCreatedViewModel> CreateOrder(CheckOutInfoInput checkOutInfoInput)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderViewModel>> GetOrders()
        {
            throw new NotImplementedException();
        }

        public Task SuspendOrder(CheckOutInfoInput checkOutInfoInput)
        {
            throw new NotImplementedException();
        }
    }
}
