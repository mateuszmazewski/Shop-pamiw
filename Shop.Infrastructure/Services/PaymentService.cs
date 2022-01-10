using Shop.Core.Domain;
using Shop.Core.Repositories;
using Shop.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task Add(Payment payment)
        {
            if (payment == null)
            {
                throw new ArgumentNullException("payment cannot be null");
            }
            payment.CreatedAt = DateTime.Now;
            payment.UpdatedAt = DateTime.Now;
            await _paymentRepository.AddAsync(payment);
        }

        public async Task<IEnumerable<PaymentDTO>> BrowseAll()
        {
            var p = await _paymentRepository.BrowseAllAsync();

            return p == null ? null : p.Select(x => new PaymentDTO()
            {
                Id = x.Id,
                Order = x.Order,
                OrderId = x.OrderId,
                Amount = x.Amount,
                Status = x.Status,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                PaymentMethod = x.PaymentMethod
            });
        }

        public async Task<PaymentDTO> GetByFilter(int orderId)
        {
            var p = await _paymentRepository.GetByFilterAsync(orderId);

            return p == null ? null : new PaymentDTO()
            {
                Id = p.Id,
                Order = p.Order,
                OrderId = p.OrderId,
                Amount = p.Amount,
                Status = p.Status,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                PaymentMethod = p.PaymentMethod
            };
        }

        public async Task Delete(int id)
        {
            await _paymentRepository.DelAsync(id);
        }

        public async Task<PaymentDTO> Get(int id)
        {
            var p = await _paymentRepository.GetAsync(id);

            return p == null ? null : new PaymentDTO()
            {
                Id = p.Id,
                Order = p.Order,
                OrderId = p.OrderId,
                Amount = p.Amount,
                Status = p.Status,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                PaymentMethod = p.PaymentMethod
            };
        }

        public async Task Update(Payment payment)
        {
            if (payment == null)
            {
                throw new ArgumentNullException("payment cannot be null");
            }
            await _paymentRepository.UpdateAsync(payment);
        }
    }
}
