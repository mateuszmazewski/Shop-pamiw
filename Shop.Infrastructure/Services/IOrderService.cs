using Shop.Core.Domain;
using Shop.Infrastructure.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> BrowseAll();
        Task<IEnumerable<OrderDTO>> BrowseAllByFilter(int customerId);
        Task<OrderDTO> Get(int id);
        Task Add(Order order);
        Task Delete(int id);
        Task Update(Order order);
    }
}
