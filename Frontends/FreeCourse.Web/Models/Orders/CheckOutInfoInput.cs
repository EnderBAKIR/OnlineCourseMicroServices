using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models.Orders
{
    public class CheckOutInfoInput
    {

        [Display(Name ="Şehir")]
        public string Province { get; set; }
        [Display(Name = "İlçe")]
        public string Disctrict { get; set; }
        [Display(Name = "Cadde")]
        public string Street { get; set; }
        [Display(Name = "Posta Kodu")]
        public string ZipCode { get; set; }
        [Display(Name = "Tam Adres")]
        public string Line { get; set; }
        [Display(Name = "Kartın Sahibi İsmi")]
        public string CardName { get; set; }
        [Display(Name = "Kart Numarası")]
        public string CardNumber { get; set; }
        [Display(Name = "Son Kullanım Tarihi")]
        public string Expiration { get; set; }
        [Display(Name = "Güvenlik Numarası")]
        public string CVV { get; set; }
        
    }
}
