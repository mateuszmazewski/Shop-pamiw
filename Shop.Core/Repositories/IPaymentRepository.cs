using Shop.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Core.Repositories
{
    public interface IPaymentRepository
    {
        Task AddAsync(Payment payment);
        Task UpdateAsync(Payment payment);
        Task DelAsync(int id);
        Task<Payment> GetAsync(int id);
        Task<IEnumerable<Payment>> BrowseAllAsync();
        Task<Payment> GetByFilterAsync(int orderId);
    }
}
