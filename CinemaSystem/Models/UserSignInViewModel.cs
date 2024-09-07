using System.ComponentModel.DataAnnotations;

namespace CinemaSystem.Models
{
    public class UserSignInViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adını Giriniz.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lütfen şifre giriniz.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
