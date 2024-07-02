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
        public Task<List<Product>> GetProducts()
            => ProductDAO.Instance.GetProducts();

        public Task<Product> GetProductById(int id)
            => ProductDAO.Instance.GetProductById(id);

        public Task AddProduct(Product product)
            => ProductDAO.Instance.AddProduct(product);

        public Task UpdateProduct(Product product)
            => ProductDAO.Instance.UpdateProduct(product);

        public Task RemoveProduct(int id)
            => ProductDAO.Instance.RemoveProduct(id);
    }
}
