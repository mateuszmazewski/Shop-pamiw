using Shop.Core.Domain;
using Shop.Infrastructure.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Services
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItemDTO>> BrowseAll();
        Task<IEnumerable<OrderItemDTO>> BrowseAllByFilter(int orderId);
        Task<OrderItemDTO> Get(int id);
        Task Add(OrderItem orderItem);
        Task Delete(int id);
        Task Update(OrderItem orderItem);
    }
}
