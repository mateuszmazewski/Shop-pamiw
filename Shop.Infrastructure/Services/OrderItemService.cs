using Shop.Core.Domain;
using Shop.Core.Repositories;
using Shop.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemService(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public async Task Add(OrderItem orderItem)
        {
            if (orderItem == null)
            {
                throw new ArgumentNullException("orderItem cannot be null");
            }
            await _orderItemRepository.AddAsync(orderItem);
        }

        public async Task<IEnumerable<OrderItemDTO>> BrowseAll()
        {
            var o = await _orderItemRepository.BrowseAllAsync();

            return o == null ? null : o.Select(x => new OrderItemDTO()
            {
                Id = x.Id,
                Order = x.Order,
                Product = x.Product,
                Quantity = x.Quantity
            });
        }

        public async Task<IEnumerable<OrderItemDTO>> BrowseAllByFilter(int orderId)
        {
            var o = await _orderItemRepository.BrowseAllByFilterAsync(orderId);

            return o == null ? null : o.Select(x => new OrderItemDTO()
            {
                Id = x.Id,
                Order = x.Order,
                Product = x.Product,
                Quantity = x.Quantity
            });
        }

        public async Task Delete(int id)
        {
            await _orderItemRepository.DelAsync(id);
        }

        public async Task<OrderItemDTO> Get(int id)
        {
            var o = await _orderItemRepository.GetAsync(id);

            return o == null ? null : new OrderItemDTO()
            {
                Id = o.Id,
                Order = o.Order,
                Product = o.Product,
                Quantity = o.Quantity
            };
        }

        public async Task Update(OrderItem orderItem)
        {
            if (orderItem == null)
            {
                throw new ArgumentNullException("orderItem cannot be null");
            }
            await _orderItemRepository.UpdateAsync(orderItem);
        }
    }
}
