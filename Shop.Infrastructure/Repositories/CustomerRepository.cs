using Shop.Core.Domain;
using Shop.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private AppDbContext _appDbContext;

        public CustomerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Customer customer)
        {
            try
            {
                _appDbContext.Add(customer);
                _appDbContext.SaveChanges();
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        public async Task<IEnumerable<Customer>> BrowseAllAsync()
        {
            return await Task.FromResult(_appDbContext.Customer);
        }

        public async Task<IEnumerable<Customer>> BrowseAllByFilterAsync(string name, string surname)
        {
            IEnumerable<Customer> c;
            if (name == null && surname != null)
            {
                c = _appDbContext.Customer.Where(x => x.Surname.Contains(surname));
            }
            else if (name != null && surname == null)
            {
                c = _appDbContext.Customer.Where(x => x.Name.Contains(name));
            }
            else if (name != null && surname != null)
            {
                c = _appDbContext.Customer.Where(x => x.Name.Contains(name) && x.Surname.Contains(surname));
            }
            else
            {
                c = _appDbContext.Customer;
            }

            return await Task.FromResult(c);
        }

        public async Task DelAsync(int id)
        {
            try
            {
                _appDbContext.Remove(_appDbContext.Customer.FirstOrDefault(x => x.Id == id));
                _appDbContext.SaveChanges();
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        public async Task<Customer> GetAsync(int id)
        {
            var c = _appDbContext.Customer.FirstOrDefault(x => x.Id == id);

            return await Task.FromResult(c);
        }

        public async Task UpdateAsync(Customer customer)
        {
            try
            {
                var c = _appDbContext.Customer.FirstOrDefault(x => x.Id == customer.Id);
                if (c != null)
                {
                    c.Name = customer.Name;
                    c.Surname = customer.Surname;
                    c.Address = customer.Address;
                    c.DateOfBirth = customer.DateOfBirth;

                    _appDbContext.SaveChanges();
                    await Task.CompletedTask;
                }
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }
    }
}
