using Shop.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Core.Repositories
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DelAsync(int id);
        Task<Customer> GetAsync(int id);
        Task<IEnumerable<Customer>> BrowseAllAsync();
        Task<IEnumerable<Customer>> BrowseAllByFilterAsync(string name, string surname);
    }
}
