using Shop.Core.Domain;
using Shop.Core.Repositories;
using Shop.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task Add(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("customer cannot be null");
            }
            await _customerRepository.AddAsync(customer);
        }

        public async Task<IEnumerable<CustomerDTO>> BrowseAll()
        {
            var c = await _customerRepository.BrowseAllAsync();

            return c == null ? null : c.Select(x => new CustomerDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
                Address = x.Address,
                DateOfBirth = x.DateOfBirth
            });
        }

        public async Task<IEnumerable<CustomerDTO>> BrowseAllByFilter(string name, string surname)
        {
            var c = await _customerRepository.BrowseAllByFilterAsync(name, surname);

            return c == null ? null : c.Select(x => new CustomerDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
                Address = x.Address,
                DateOfBirth = x.DateOfBirth
            });
        }

        public async Task Delete(int id)
        {
            await _customerRepository.DelAsync(id);
        }

        public async Task<CustomerDTO> Get(int id)
        {
            var c = await _customerRepository.GetAsync(id);

            return c == null ? null : new CustomerDTO()
            {
                Id = c.Id,
                Name = c.Name,
                Surname = c.Surname,
                Address = c.Address,
                DateOfBirth = c.DateOfBirth
            };
        }

        public async Task Update(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("customer cannot be null");
            }
            await _customerRepository.UpdateAsync(customer);
        }
    }
}
