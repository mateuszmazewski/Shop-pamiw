using System.ComponentModel.DataAnnotations;

namespace Shop.WebApp.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana.")]
        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }

        // Identity domyślnie stosuje politykę silnych haseł
        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [MinLength(8, ErrorMessage = "Musi mieć co najmniej 8 znaków")]
        [MaxLength(30, ErrorMessage = "Może mieć max. 30 znaków")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d.*)(?=.*\W.*)[a-zA-Z0-9\S]{8,30}$",
            ErrorMessage = "Musi zawierać dużą literę, małą literę, cyfrę i znak specjalny")]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
