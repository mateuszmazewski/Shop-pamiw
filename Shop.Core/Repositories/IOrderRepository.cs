using Shop.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Core.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DelAsync(int id);
        Task<Order> GetAsync(int id);
        Task<IEnumerable<Order>> BrowseAllAsync();
        Task<IEnumerable<Order>> BrowseAllByFilterAsync(int customerId);
    }
}
