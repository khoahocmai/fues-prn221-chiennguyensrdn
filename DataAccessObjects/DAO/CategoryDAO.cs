using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessObjects.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance = null;
        public static readonly object Lock = new object();
        private CategoryDAO() { }
        public static CategoryDAO Instance
        {
            get
            {
                lock (Lock)
                {
                    if (instance == null)
                    {
                        instance = new CategoryDAO();
                    }
                    return instance;
                }
            }
        }

        public async Task<List<Category>> GetCategories()
        {
            using var db = new FUESManagementContext();
            return await db.Categories
                .Include(c => c.Products)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            using var db = new FUESManagementContext();
            return await db.Categories.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddCategory(Category category)
        {
            using var db = new FUESManagementContext();
            db.Categories.AddAsync(category);
            await db.SaveChangesAsync();
        }

        public async Task UpdateCategory(Category category)
        {
            using var db = new FUESManagementContext();
            var existingCategory = await db.Categories.FindAsync(category.Id);
            if (existingCategory == null)
            {
                throw new ArgumentException("Category not found");
            }

            existingCategory.Name = category.Name;

            db.Categories.Update(existingCategory);
            await db.SaveChangesAsync();
        }

        public async Task RemoveCategory(int id)
        {
            using var db = new FUESManagementContext();
            var category = await db.Categories.FindAsync(id);
            if (category != null)
            {
                db.Categories.Remove(category);
                await db.SaveChangesAsync();
            }   
        }
    }
}
