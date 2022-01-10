using Shop.Core.Domain;
using Shop.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Product product)
        {
            try
            {
                _appDbContext.Add(product);
                _appDbContext.SaveChanges();
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        public async Task<IEnumerable<Product>> BrowseAllAsync()
        {
            return await Task.FromResult(_appDbContext.Product);
        }

        public async Task<IEnumerable<Product>> BrowseAllByFilterAsync(string name)
        {
            IEnumerable<Product> p;
            if (name != null)
            {
                p = _appDbContext.Product.Where(x => x.Name.Contains(name));
            }
            else
            {
                p = _appDbContext.Product;
            }

            return await Task.FromResult(p);
        }

        public async Task DelAsync(int id)
        {
            try
            {
                _appDbContext.Remove(_appDbContext.Product.FirstOrDefault(x => x.Id == id));
                _appDbContext.SaveChanges();
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        public async Task<Product> GetAsync(int id)
        {
            var p = _appDbContext.Product.FirstOrDefault(x => x.Id == id);

            return await Task.FromResult(p);
        }

        public async Task UpdateAsync(Product product)
        {
            try
            {
                var p = _appDbContext.Product.FirstOrDefault(x => x.Id == product.Id);
                if (p != null)
                {
                    p.Name = product.Name;
                    p.Price = product.Price;
                    p.Stock = product.Stock;
                    p.UnitOfMeasurement = product.UnitOfMeasurement;

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
