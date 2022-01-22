using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.WebApp.Models
{
    public class CustomerVM
    {
        public int Id { get; set; }

        [Display(Name = "Imię")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        [Required]
        public string Surname { get; set; }

        [Display(Name = "Adres")]
        [Required]
        public string Address { get; set; }

        [Display(Name = "Data urodzenia")]
        [Required]
        public DateTime DateOfBirth { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name} {Surname}";
        }
    }
}
