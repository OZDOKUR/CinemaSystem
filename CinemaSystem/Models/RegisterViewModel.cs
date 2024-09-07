using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CinemaSystem.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "İsim zorunludur.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Doğum Tarihi zorunludur.")]
        public DateTime BirthDate { get; set; }
        
        [Required(ErrorMessage = "Cinsiyet zorunludur.")]
        public string Gender { get; set; }

        public List<SelectListItem> GenderOptions { get; set; } = new List<SelectListItem>
    {
        new SelectListItem { Value = "male", Text = "Erkek" },
        new SelectListItem { Value = "female", Text = "Kadın" },
        new SelectListItem { Value = "other", Text = "Diğer" }
    };

        [Required(ErrorMessage = "E-posta zorunludur.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon Numarası zorunludur.")]
        [RegularExpression(@"^\d{9,15}$", ErrorMessage = "Geçersiz telefon numarası. Sadece rakamlar ve 10-15 haneli olmalı.")]
        public string PhoneNumber { get; set; }


        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Lütfen şifre giriniz.")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Şifre en az 8 karakterli, bir büyük bir küçük ve özel karakter içermelidir")]
        public string Password { get; set; }

        [Display(Name = "Şifre tekrar")]
        [Required(ErrorMessage = "Lütfen şifre tekrarı giriniz.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor!")]
        public string ConfirmPassword { get; set; }
    }
}
