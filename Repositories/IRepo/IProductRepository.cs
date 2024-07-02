using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepo
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product product);
        Task<Product> GetByIdAsync(int id);
        Task<List<Product>> GetAllAsync();
        Task<Product> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
        Task<List<Product>> SearchAsync(string query);
        Task<List<Product>> FilterAsync(int? categoryId, decimal? minPrice, decimal? maxPrice);
    }
}
