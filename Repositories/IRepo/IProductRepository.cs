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
        Task<List<Product>> GetProducts();
        Task<Product> GetProductById(int id);
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task RemoveProduct(int id);
    }
}
