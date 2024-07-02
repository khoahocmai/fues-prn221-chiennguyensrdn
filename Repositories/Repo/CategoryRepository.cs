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
    public class CategoryRepository : ICategoryRepository
    {
        public Task<List<Category>> GetCategories()
            => CategoryDAO.Instance.GetCategories();

        public Task<Category> GetCategoryById(int id)
            => CategoryDAO.Instance.GetCategoryById(id);

        public Task AddCategory(Category category)
            => CategoryDAO.Instance.AddCategory(category);

        public Task UpdateCategory(Category category)
            => CategoryDAO.Instance.UpdateCategory(category);

        public Task RemoveCategory(int id)
            => CategoryDAO.Instance.RemoveCategory(id);
    }
}
