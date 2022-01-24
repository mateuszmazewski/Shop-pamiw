using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.WebApp.Models
{
    public class CustomerVM
    {
        public int Id { get; set; }

        [Display(Name = "Imię")]
        [Required(ErrorMessage = "Imię jest wymagane")]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        public string Surname { get; set; }

        [Display(Name = "Adres")]
        [Required(ErrorMessage = "Adres jest wymagany")]
        public string Address { get; set; }

        [Display(Name = "Data urodzenia")]
        [Required(ErrorMessage = "Data urodzenia jest wymagana")]
        public DateTime DateOfBirth { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name} {Surname}";
        }
    }
}
