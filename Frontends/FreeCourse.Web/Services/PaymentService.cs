using FreeCourse.Web.Models.FakePayments;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services
{
    public class PaymentService : IPaymentService
    {
        public Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput)
        {
            throw new NotImplementedException();
        }
    }
}
