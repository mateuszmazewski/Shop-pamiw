using Shop.Core.Domain;
using Shop.Infrastructure.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDTO>> BrowseAll();
        Task<PaymentDTO> GetByFilter(int orderId);
        Task<PaymentDTO> Get(int id);
        Task Add(Payment payment);
        Task Delete(int id);
        Task Update(Payment payment);
    }
}
