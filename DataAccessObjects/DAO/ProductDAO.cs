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
        private readonly FUESManagementContext _context;

        public ProductDAO(FUESManagementContext context)
        {
            _context = context;
        }

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
            return await _context.Products
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
            return await _context.Products
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
            var existingProduct = await _context.Products.FindAsync(product.Id);
            if (existingProduct == null)
            {
                throw new ArgumentException("Product not found");
            }

            existingProduct.Title = product.Title;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.UpdatedAt = DateTime.Now;

            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();
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
    }
}
