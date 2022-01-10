using Shop.Core.Domain;
using Shop.Core.Repositories;
using Shop.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Add(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException("order cannot be null");
            }
            order.CreatedAt = DateTime.Now;
            order.UpdatedAt = DateTime.Now;
            await _orderRepository.AddAsync(order);
        }

        public async Task<IEnumerable<OrderDTO>> BrowseAll()
        {
            var o = await _orderRepository.BrowseAllAsync();

            return o == null ? null : o.Select(x => new OrderDTO()
            {
                Id = x.Id,
                Customer = x.Customer,
                Payment = x.Payment,
                PaymentId = x.PaymentId,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            });
        }

        public async Task<IEnumerable<OrderDTO>> BrowseAllByFilter(int customerId)
        {
            var o = await _orderRepository.BrowseAllByFilterAsync(customerId);

            return o == null ? null : o.Select(x => new OrderDTO()
            {
                Id = x.Id,
                Customer = x.Customer,
                Payment = x.Payment,
                PaymentId = x.PaymentId,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            });
        }

        public async Task Delete(int id)
        {
            await _orderRepository.DelAsync(id);
        }

        public async Task<OrderDTO> Get(int id)
        {
            var o = await _orderRepository.GetAsync(id);

            return o == null ? null : new OrderDTO()
            {
                Id = o.Id,
                Customer = o.Customer,
                Payment = o.Payment,
                PaymentId = o.PaymentId,
                CreatedAt = o.CreatedAt,
                UpdatedAt = o.UpdatedAt
            };
        }

        public async Task Update(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException("order cannot be null");
            }
            order.UpdatedAt = DateTime.Now;
            await _orderRepository.UpdateAsync(order);
        }
    }
}
