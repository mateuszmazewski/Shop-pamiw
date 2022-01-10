using System;

namespace Shop.Infrastructure.Commands
{
    public class UpdateCustomer
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
