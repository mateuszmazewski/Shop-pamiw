using Shop.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Core.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DelAsync(int id);
        Task<Product> GetAsync(int id);
        Task<IEnumerable<Product>> BrowseAllAsync();
        Task<IEnumerable<Product>> BrowseAllByFilterAsync(string name);
    }
}
