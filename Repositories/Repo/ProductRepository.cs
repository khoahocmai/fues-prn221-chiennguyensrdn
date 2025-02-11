﻿using BusinessObjects;
using DataAccessObjects.DAO;
using Repositories.IRepo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Repo
{
    public class ProductRepository : IProductRepository
    {
        public Task<List<Product>> GetProducts()
            => ProductDAO.Instance.GetProducts();

        public Task<List<Product>> GetProductsBySellerId(int sellerId)
            => ProductDAO.Instance.GetProductsBySellerId(sellerId);

        public Task<Product> GetProductById(int id)
            => ProductDAO.Instance.GetProductById(id);

        public Task AddProduct(Product product)
            => ProductDAO.Instance.AddProduct(product);

        public Task UpdateProduct(Product product)
            => ProductDAO.Instance.UpdateProduct(product);

        public Task RemoveProduct(int id)
            => ProductDAO.Instance.RemoveProduct(id);

        public Task<List<Product>> GetProductsByCategoryId(int categoryId)
            => ProductDAO.Instance.GetProductsByCategoryId(categoryId);

        public Task<int> GetTotalProducts()
            => ProductDAO.Instance.GetTotalProducts();

        public Task<List<Product>> GetProductBanned()
            => ProductDAO.Instance.GetProductBanned();

        public Task UpdateProductStatus(int productId, string status)
            => ProductDAO.Instance.UpdateProductStatus(productId, status);
    }
}
