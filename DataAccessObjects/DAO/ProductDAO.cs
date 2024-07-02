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

        public async Task<Product> CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetByIdAsync(int id)
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

        public async Task<List<Product>> GetAllAsync()
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

        public async Task<Product> UpdateAsync(Product product)
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
            return existingProduct;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Product>> SearchAsync(string query)
        {
            return await _context.Products
                .Where(p => p.Title.Contains(query) || p.Description.Contains(query))
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .ToListAsync();
        }

        public async Task<List<Product>> FilterAsync(int? categoryId, decimal? minPrice, decimal? maxPrice)
        {
            var query = _context.Products.AsQueryable();

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice);
            }

            return await query
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .ToListAsync();
        }
    }
}
