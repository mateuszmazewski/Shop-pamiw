using Shop.Core.Domain;
using Shop.Infrastructure.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> BrowseAll();
        Task<IEnumerable<ProductDTO>> BrowseAllByFilter(string name);
        Task<ProductDTO> Get(int id);
        Task Add(Product product);
        Task Delete(int id);
        Task Update(Product product);
    }
}