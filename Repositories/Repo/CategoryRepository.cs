using BusinnessObjects;
using DataAccessObjects.DAO;
using Repositories.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repo
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryDAO _categoryDAO;

        public CategoryRepository(CategoryDAO categoryDAO)
        {
            _categoryDAO = categoryDAO;
        }

        public Task<Category> CreateAsync(Category category)
        {
            return _categoryDAO.CreateAsync(category);
        }

        public Task<List<Category>> GetAllAsync()
        {
            return _categoryDAO.GetAllAsync();
        }

        public Task<Category> UpdateAsync(Category category)
        {
            return _categoryDAO.UpdateAsync(category);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _categoryDAO.DeleteAsync(id);
        }
    }
}
