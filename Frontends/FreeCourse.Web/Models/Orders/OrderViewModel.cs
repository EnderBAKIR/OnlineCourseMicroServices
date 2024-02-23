namespace FreeCourse.Web.Models.Orders
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        //Ödeme geçmişinde adrese gerek olmadığı için alınmadı;
        //public AddressDto Address { get; set; }

        public string BuyerId { get; set; }

        public List<OrderItemViewModel> OrderItems { get; set; }
    }
}
