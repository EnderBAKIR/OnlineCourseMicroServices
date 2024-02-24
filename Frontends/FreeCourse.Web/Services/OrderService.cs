using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Services;
using FreeCourse.Web.Models.FakePayments;
using FreeCourse.Web.Models.Orders;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly IPaymentService _paymentService;
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public OrderService(HttpClient httpClient, IPaymentService paymentService, IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _httpClient = httpClient;
            _paymentService = paymentService;
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<OrderCreatedViewModel> CreateOrder(CheckOutInfoInput checkOutInfoInput)
        {
            var basket = await _basketService.Get();

            var paymentInfoInput = new PaymentInfoInput()
            {
                CardName = checkOutInfoInput.CardName,
                CardNumber = checkOutInfoInput.CardNumber,
                Expiration = checkOutInfoInput.Expiration,
                CVV = checkOutInfoInput.CVV,
                TotalPrice = basket.TotalPrice
            };

            var responsePayment = await _paymentService.ReceivePayment(paymentInfoInput);

            if (!responsePayment)
            {
                return new OrderCreatedViewModel() { Error = "Ödeme alınamadı", IsSuccessful = false };
            }

            var orderCreateInput = new OrderCreateInput()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new AddressCreateInput { Province = checkOutInfoInput.Province, Disctrict = checkOutInfoInput.Disctrict, Street = checkOutInfoInput.Street, Line = checkOutInfoInput.Line, ZipCode = checkOutInfoInput.ZipCode }

            };
            basket.BasketItems.ForEach(x =>
            {
                var orderItem = new OrderItemCreateInput { ProductId = x.CourseId, Price = x.GetCurrentPrice, PictureUrl = "", ProductName = x.CourseName };
                orderCreateInput.OrderItems.Add(orderItem);
            });


            var response = await _httpClient.PostAsJsonAsync<OrderCreateInput>("orders", orderCreateInput);

            if (!response.IsSuccessStatusCode)
            {
                return new OrderCreatedViewModel() { Error = "Sipariş oluşturulurken bir problem meydana geldi", IsSuccessful = false };
            }

            var orderCreatedViewModel = await response.Content.ReadFromJsonAsync<Response<OrderCreatedViewModel>>();

            orderCreatedViewModel.Data.IsSuccessful = true;
         await   _basketService.Delete();
            return orderCreatedViewModel.Data;

        }


        public async Task<List<OrderViewModel>> GetOrders()
        {
            var response = await _httpClient.GetFromJsonAsync<Response<List<OrderViewModel>>>("orders");

            return response.Data;
        }


        public async Task<OrderSuspendViewModel> SuspendOrder(CheckOutInfoInput checkOutInfoInput)
        {


            var basket = await _basketService.Get();

            var orderCreateInput = new OrderCreateInput()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new AddressCreateInput { Province = checkOutInfoInput.Province, Disctrict = checkOutInfoInput.Disctrict, Street = checkOutInfoInput.Street, Line = checkOutInfoInput.Line, ZipCode = checkOutInfoInput.ZipCode }

            };
            basket.BasketItems.ForEach(x =>
            {
                var orderItem = new OrderItemCreateInput { ProductId = x.CourseId, Price = x.GetCurrentPrice, PictureUrl = "", ProductName = x.CourseName };
                orderCreateInput.OrderItems.Add(orderItem);
            });


            var paymentInfoInput = new PaymentInfoInput()
            {
                CardName = checkOutInfoInput.CardName,
                CardNumber = checkOutInfoInput.CardNumber,
                Expiration = checkOutInfoInput.Expiration,
                CVV = checkOutInfoInput.CVV,
                TotalPrice = basket.TotalPrice,
                Order = orderCreateInput
            };


            var responsePayment = await _paymentService.ReceivePayment(paymentInfoInput);

            if (!responsePayment)
            {
                return new OrderSuspendViewModel() { Error = "Ödeme alınamadı", IsSuccessful = false };
            }
            await _basketService.Delete();
            return new OrderSuspendViewModel() {IsSuccessful = true};

        }
    }
}
