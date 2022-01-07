using Shop.Core.Domain;
using Shop.Infrastructure.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> BrowseAll();
        Task<IEnumerable<CustomerDTO>> BrowseAllByFilter(string name, string surname);
        Task<CustomerDTO> Get(int id);
        Task Add(Customer customer);
        Task Delete(int id);
        Task Update(Customer customer);
    }
}
