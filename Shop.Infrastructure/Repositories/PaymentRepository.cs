using Shop.Core.Domain;
using Shop.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private AppDbContext _appDbContext;

        public PaymentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Payment payment)
        {
            try
            {
                _appDbContext.Add(payment);
                _appDbContext.SaveChanges();
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        public async Task<IEnumerable<Payment>> BrowseAllAsync()
        {
            return await Task.FromResult(_appDbContext.Payment);
        }

        public async Task<Payment> GetByFilterAsync(int orderId)
        {
            Payment p = _appDbContext.Payment.FirstOrDefault(x => x.Order.Id.Equals(orderId));

            return await Task.FromResult(p);
        }

        public async Task DelAsync(int id)
        {
            try
            {
                _appDbContext.Remove(_appDbContext.Payment.FirstOrDefault(x => x.Id == id));
                _appDbContext.SaveChanges();
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        public async Task<Payment> GetAsync(int id)
        {
            var p = _appDbContext.Payment.FirstOrDefault(x => x.Id == id);

            return await Task.FromResult(p);
        }

        public async Task UpdateAsync(Payment payment)
        {
            try
            {
                var p = _appDbContext.Payment.FirstOrDefault(x => x.Id == payment.Id);
                if (p != null)
                {
                    p.Order = payment.Order;
                    p.OrderId = payment.OrderId;
                    p.Amount = payment.Amount;
                    p.Status = payment.Status;
                    p.CreatedAt = payment.CreatedAt;
                    p.UpdatedAt = payment.UpdatedAt;
                    p.PaymentMethod = payment.PaymentMethod;

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
