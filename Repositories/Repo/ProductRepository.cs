using BusinessObjects;
using DataAccessObjects.DAO;
using Repositories.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repo
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDAO _productDAO;

        public ProductRepository(ProductDAO productDAO)
        {
            _productDAO = productDAO;
        }

        public Task<Product> CreateAsync(Product product)
        {
            return _productDAO.CreateAsync(product);
        }

        public Task<Product> GetByIdAsync(int id)
        {
            return _productDAO.GetByIdAsync(id);
        }

        public Task<List<Product>> GetAllAsync()
        {
            return _productDAO.GetAllAsync();
        }

        public Task<Product> UpdateAsync(Product product)
        {
            return _productDAO.UpdateAsync(product);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _productDAO.DeleteAsync(id);
        }

        public Task<List<Product>> SearchAsync(string query)
        {
            return _productDAO.SearchAsync(query);
        }

        public Task<List<Product>> FilterAsync(int? categoryId, decimal? minPrice, decimal? maxPrice)
        {
            return _productDAO.FilterAsync(categoryId, minPrice, maxPrice);
        }
    }
}
