using Shop.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Core.Repositories
{
    public interface IOrderItemRepository
    {
        Task AddAsync(OrderItem orderItem);
        Task UpdateAsync(OrderItem orderItem);
        Task DelAsync(int id);
        Task<OrderItem> GetAsync(int id);
        Task<IEnumerable<OrderItem>> BrowseAllAsync();
        Task<IEnumerable<OrderItem>> BrowseAllByFilterAsync(int orderId);
    }
}
