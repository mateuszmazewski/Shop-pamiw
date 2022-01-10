using Shop.Core.Domain;
using Shop.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private AppDbContext _appDbContext;

        public OrderItemRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(OrderItem orderItem)
        {
            try
            {
                _appDbContext.Add(orderItem);
                _appDbContext.SaveChanges();
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        public async Task<IEnumerable<OrderItem>> BrowseAllAsync()
        {
            return await Task.FromResult(_appDbContext.OrderItem);
        }

        public async Task<IEnumerable<OrderItem>> BrowseAllByFilterAsync(int orderId)
        {
            IEnumerable<OrderItem> o = _appDbContext.OrderItem.Where(x => x.Id.Equals(orderId));

            return await Task.FromResult(o);
        }

        public async Task DelAsync(int id)
        {
            try
            {
                _appDbContext.Remove(_appDbContext.OrderItem.FirstOrDefault(x => x.Id == id));
                _appDbContext.SaveChanges();
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        public async Task<OrderItem> GetAsync(int id)
        {
            var o = _appDbContext.OrderItem.FirstOrDefault(x => x.Id == id);

            return await Task.FromResult(o);
        }

        public async Task UpdateAsync(OrderItem orderItem)
        {
            try
            {
                var o = _appDbContext.OrderItem.FirstOrDefault(x => x.Id == orderItem.Id);
                if (o != null)
                {
                    o.Order = orderItem.Order;
                    o.Product = orderItem.Product;
                    o.Quantity = orderItem.Quantity;

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
