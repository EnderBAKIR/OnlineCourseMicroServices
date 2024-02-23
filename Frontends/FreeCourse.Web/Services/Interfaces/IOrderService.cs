using FreeCourse.Web.Models.Orders;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IOrderService
    {

        //Senkron İletişim direkt microservicese istek yapılacak
        Task<OrderCreatedViewModel> CreateOrder(CheckOutInfoInput checkOutInfoInput);

        //Asenktron işetişim - Sipariş Bilgileri RabbitMq Ya Gönderilecek.
        Task SuspendOrder(CheckOutInfoInput checkOutInfoInput);

        Task<List<OrderViewModel>> GetOrders();
    }
}
