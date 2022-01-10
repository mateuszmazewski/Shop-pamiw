using Shop.Core.Domain;
using Shop.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private AppDbContext _appDbContext;

        public OrderRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Order order)
        {
            try
            {
                _appDbContext.Add(order);
                _appDbContext.SaveChanges();
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        public async Task<IEnumerable<Order>> BrowseAllAsync()
        {
            return await Task.FromResult(_appDbContext.Order);
        }

        public async Task<IEnumerable<Order>> BrowseAllByFilterAsync(int customerId)
        {
            IEnumerable<Order> o = _appDbContext.Order.Where(x => x.Customer.Id.Equals(customerId));

            return await Task.FromResult(o);
        }

        public async Task DelAsync(int id)
        {
            try
            {
                _appDbContext.Remove(_appDbContext.Order.FirstOrDefault(x => x.Id == id));
                _appDbContext.SaveChanges();
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        public async Task<Order> GetAsync(int id)
        {
            var o = _appDbContext.Order.FirstOrDefault(x => x.Id == id);

            return await Task.FromResult(o);
        }

        public async Task UpdateAsync(Order order)
        {
            try
            {
                var o = _appDbContext.Order.FirstOrDefault(x => x.Id == order.Id);
                if (o != null)
                {
                    o.Customer = order.Customer;
                    o.Payment = order.Payment;
                    o.PaymentId = order.PaymentId;
                    o.CreatedAt = order.CreatedAt;
                    o.UpdatedAt = order.UpdatedAt;

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
