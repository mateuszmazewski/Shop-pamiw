using System;

namespace Shop.Infrastructure.Commands
{
    public class CreateCustomer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
