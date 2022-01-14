using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.WebApp.Models
{
    public class CustomerVM
    {
        public int Id { get; set; }

        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Display(Name = "Data urodzenia")]
        public DateTime DateOfBirth { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Name} {Surname}";
        }
    }
}
