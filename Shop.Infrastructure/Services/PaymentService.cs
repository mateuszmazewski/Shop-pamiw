using Shop.Core.Domain;
using Shop.Core.Repositories;
using Shop.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;

        public PaymentService(IPaymentRepository paymentRepository, IOrderRepository orderRepository)
        {
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
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
            Order o = await _orderRepository.GetAsync(payment.OrderId);
            if (o != null)
            {
                o.PaymentId = payment.Id;
                await _orderRepository.UpdateAsync(o);
            }

            string message = $"Pojawiła się nowa płatność:<br />" +
                $"ID zamówienia: {payment.OrderId}<br />" +
                $"Kwota: {payment.Amount} zł<br />" +
                $"Status: {Payment.PaymentStatusDisplayName(payment.Status)}<br />" +
                $"Metoda płatności: {Payment.PaymentMethodDisplayName(payment.PaymentMethod)}<br />" +
                $"Data utworzenia: {payment.CreatedAt}<br />" +
                $"Ta wiadomość została wygenerowana automatycznie.";
            sendEmail("mateusz.mazewski.stud@pw.edu.pl", "Nowa płatność w systemie", message);
        }

        private void sendEmail(string toWhom, string subject, string mailBody)
        {
            string to = toWhom;
            string from = "shopmanagementapp@gmail.com";
            MailMessage mail = new MailMessage(from, to);

            mail.Subject = subject;
            mail.Body = mailBody;
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); // Gmail smtp    
            NetworkCredential credentials = new NetworkCredential("shopmanagementapp@gmail.com", "SuperTajne.123");
            client.EnableSsl = true;
            client.Credentials = credentials;
            try
            {
                client.Send(mail);
            }

            catch (Exception ex)
            {
                throw ex;
            }
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
            Payment p = await _paymentRepository.GetAsync(id);
            if (p != null)
            {
                Order o = await _orderRepository.GetAsync(p.OrderId);
                if (o != null)
                {
                    o.PaymentId = 0;
                    await _orderRepository.UpdateAsync(o);
                }
            }
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
            payment.UpdatedAt = DateTime.Now;
            await _paymentRepository.UpdateAsync(payment);
            Order o = await _orderRepository.GetAsync(payment.OrderId);
            if (o != null)
            {
                o.PaymentId = payment.Id;
                await _orderRepository.UpdateAsync(o);
            }
        }
    }
}
