using System.ComponentModel.DataAnnotations;

namespace FreeCouse.IdentityServer.Dtos
{
    public class SignUpDto
    {
        [Required(ErrorMessage ="Email Boş Geçilemez")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Password Boş Geçilemez")]
        public string Password { get; set; }
        [Required(ErrorMessage = "UserName Boş Geçilemez")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "City Boş Geçilemez")]
        public string City { get; set; }
    }
}
