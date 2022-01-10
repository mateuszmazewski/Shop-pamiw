using Shop.Core.Domain;
using Shop.Core.Repositories;
using Shop.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Add(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("product cannot be null");
            }
            await _productRepository.AddAsync(product);
        }

        public async Task<IEnumerable<ProductDTO>> BrowseAll()
        {
            var p = await _productRepository.BrowseAllAsync();

            return p == null ? null : p.Select(x => new ProductDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Stock = x.Stock,
                UnitOfMeasurement = x.UnitOfMeasurement
            });
        }

        public async Task<IEnumerable<ProductDTO>> BrowseAllByFilter(string name)
        {
            var p = await _productRepository.BrowseAllByFilterAsync(name);

            return p == null ? null : p.Select(x => new ProductDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Stock = x.Stock,
                UnitOfMeasurement = x.UnitOfMeasurement
            });
        }

        public async Task Delete(int id)
        {
            await _productRepository.DelAsync(id);
        }

        public async Task<ProductDTO> Get(int id)
        {
            var p = await _productRepository.GetAsync(id);

            return p == null ? null : new ProductDTO()
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                UnitOfMeasurement = p.UnitOfMeasurement
            };
        }

        public async Task Update(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("product cannot be null");
            }
            await _productRepository.UpdateAsync(product);
        }
    }
}
