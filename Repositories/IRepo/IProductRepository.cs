using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjects;

namespace Repositories.IRepo
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
        Task<List<Product>> GetProductsBySellerId(int sellerId);
        Task<Product> GetProductById(int id);
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task RemoveProduct(int id);
        Task<List<Product>> GetProductsByCategoryId(int categoryId);
    }
}