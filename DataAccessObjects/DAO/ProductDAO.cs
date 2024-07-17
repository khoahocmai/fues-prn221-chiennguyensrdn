using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DAO
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        public static readonly object Lock = new object();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (Lock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }

        public async Task<List<Product>> GetProducts()
        {
            using var db = new FUESManagementContext();
            return await db.Products
                .Where(p => p.Status == "Pending" && p.Status != "Banned")
                 .Include(p => p.Category)
                 .Include(p => p.Seller)
                 .Include(p => p.Comments)
                 .Include(p => p.ExchangeRequests)
                 .Include(p => p.Ratings)
                 .Include(p => p.Reports)
                 .Include(p => p.Transactions)
                 .ToListAsync();
        }

        public async Task<List<Product>> GetProductsBySellerId(int sellerId)
        {
            using var db = new FUESManagementContext();
            return await db.Products
                .Where(p => p.SellerId == sellerId)
                 .Include(p => p.Category)
                 .Include(p => p.Seller)
                 .Include(p => p.Comments)
                 .Include(p => p.ExchangeRequests)
                 .Include(p => p.Ratings)
                 .Include(p => p.Reports)
                 .Include(p => p.Transactions)
                 .ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            using var db = new FUESManagementContext();
            return await db.Products
                 .Include(p => p.Category)
                 .Include(p => p.Seller)
                 .Include(p => p.Comments)
                 .Include(p => p.ExchangeRequests)
                 .Include(p => p.Ratings)
                 .Include(p => p.Reports)
                 .Include(p => p.Transactions)
                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddProduct(Product product)
        {
            using var db = new FUESManagementContext();
            await db.Products.AddAsync(product);
            await db.SaveChangesAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            using var db = new FUESManagementContext();
            var existingProduct = await db.Products.FindAsync(product.Id);
            if (existingProduct == null)
            {
                throw new ArgumentException("Product not found");
            }

            existingProduct.Title = product.Title;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.Status = product.Status;
            existingProduct.Image = product.Image;
            existingProduct.UpdatedAt = DateTime.Now;

            db.Products.Update(existingProduct);
            await db.SaveChangesAsync();
        }

        public async Task RemoveProduct(int id)
        {
            using var db = new FUESManagementContext();
            var product = await db.Products.FindAsync(id);
            if (product != null)
            {
                db.Products.Remove(product);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<Product>> GetProductsByCategoryId(int categoryId)
        {
            using var db = new FUESManagementContext();
            return await db.Products
                .Where(p => p.CategoryId == categoryId)
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .ToListAsync();
        }

        public async Task<int> GetTotalProducts()
        {
            using var db = new FUESManagementContext();
            return await db.Products.CountAsync();
        }

        public async Task<List<Product>> GetProductBanned()
        {
            using var db = new FUESManagementContext();
            var bannedProducts = await db.Bans
                .Where(b => b.EndDate == null || b.EndDate > DateTime.Now)
                .Select(b => b.Product)
                .ToListAsync();
            return bannedProducts;
        }

        public async Task UpdateProductStatus(int productId, string status)
        {
            using var db = new FUESManagementContext();
            var product = await db.Products.FindAsync(productId);
            if (product != null)
            {
                product.Status = status;
                await db.SaveChangesAsync();
            }
        }

    }
}
